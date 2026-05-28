using Confluent.Kafka;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// 1. Define Kafka Configurations
var bootstrapServers = "localhost:9092";
var topicName = "minimal-api-topic";

var producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = bootstrapServers,
    GroupId = "minimal-api-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

// 2. Register the Kafka Producer as a Singleton
builder.Services.AddSingleton(new ProducerBuilder<Null, string>(producerConfig).Build());

// 3. Register a shared thread-safe store to pass consumed messages to our GET endpoint
builder.Services.AddSingleton<ConcurrentQueue<string>>(new ConcurrentQueue<string>());

// 4. Register the Background Consumer Service
builder.Services.AddHostedService<KafkaConsumerBackgroundService>(sp =>
    new KafkaConsumerBackgroundService(
        consumerConfig,
        topicName,
        sp.GetRequiredService<ConcurrentQueue<string>>(),
        sp.GetRequiredService<ILogger<KafkaConsumerBackgroundService>>())
);

var app = builder.Build();

// ==================== MINIMAL API ENDPOINTS ====================

// POST Endpoint: Produces a message to Kafka
app.MapPost("/api/messages", async (string message, IProducer<Null, string> producer) =>
{
    if (string.IsNullOrWhiteSpace(message))
    {
        return Results.BadRequest("Message cannot be empty.");
    }

    try
    {
        var kafkaMessage = new Message<Null, string> { Value = message };
        var deliveryResult = await producer.ProduceAsync(topicName, kafkaMessage);

        return Results.Ok(new { Status = "Sent", Offset = deliveryResult.Offset.Value });
    }
    catch (ProduceException<Null, string> ex)
    {
        return Results.Problem($"Kafka delivery failed: {ex.Error.Reason}");
    }
});

// GET Endpoint: Retrieves the latest read messages from our local memory queue
app.MapGet("/api/messages/latest", (ConcurrentQueue<string> messageQueue) =>
{
    var messages = messageQueue.ToArray();
    return Results.Ok(messages);
});

app.Run();

// ==================== BACKGROUND CONSUMER WORKER ====================

public class KafkaConsumerBackgroundService : BackgroundService
{
    private readonly ConsumerConfig _config;
    private readonly string _topic;
    private readonly ConcurrentQueue<string> _messageQueue;
    private readonly ILogger<KafkaConsumerBackgroundService> _logger;

    public KafkaConsumerBackgroundService(
        ConsumerConfig config,
        string topic,
        ConcurrentQueue<string> messageQueue,
        ILogger<KafkaConsumerBackgroundService> logger)
    {
        _config = config;
        _topic = topic;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Run the consumer loop on a separate background thread
        return Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
    }

    private void StartConsumerLoop(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
        consumer.Subscribe(_topic);

        _logger.LogInformation("Kafka Background Consumer started and listening...");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // Blocking call that waits for messages from the cluster
                    var consumeResult = consumer.Consume(cancellationToken);

                    _logger.LogInformation($"Received from Kafka: {consumeResult.Message.Value}");

                    // Keep a rolling log of the last 10 messages in memory
                    _messageQueue.Enqueue(consumeResult.Message.Value);
                    if (_messageQueue.Count > 10)
                    {
                        _messageQueue.TryDequeue(out _);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"Error occurred during consumption: {ex.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Expected fallback when cancellationToken triggers closing
        }
        finally
        {
            consumer.Close();
        }
    }
}