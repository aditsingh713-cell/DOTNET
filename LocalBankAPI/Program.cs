using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
var recordsDb = new ConcurrentDictionary<int, BankRecord>(new[]
{
    // Arguments: (Id, AccountNumber, Type, Balance)
    new KeyValuePair<int, BankRecord>(1, new BankRecord(1, "ACC-9081", "Checking", 14500.23m)),
    new KeyValuePair<int, BankRecord>(2, new BankRecord(2, "ACC-4322", "Savings", 250450.00m)),
    new KeyValuePair<int, BankRecord>(3, new BankRecord(3, "LN-7761", "Auto Loan", -18200.50m)),
    new KeyValuePair<int, BankRecord>(4, new BankRecord(4, "LN-1102", "Mortgage", -415000.00m)),
    new KeyValuePair<int, BankRecord>(5, new BankRecord(5, "ACC-5541", "Investment", 89430.75m))
});

app.MapGet("/api/records", () => Results.Ok(recordsDb.Values.OrderBy(records =>records.Id)));
app.MapPost("/api/records", (CreateRecordDto input) =>
{
    int newId = recordsDb.Keys.Count > 0 ? recordsDb.Keys.Max() + 1 : 1;
    var newRecord = new BankRecord(newId, input.AccountNumber, input.Type, input.Balance);

    if (recordsDb.TryAdd(newId, newRecord))
    {
        return Results.Created($"/api/records/{newId}", newRecord);
    }
    return Results.BadRequest(new { Error = "Failed to add record." });
}); app.MapDelete("/api/records/{id:int}", (int id) =>
    recordsDb.TryRemove(id, out _)
        ? Results.Ok(new { Status = $"Record {id} dropped successfully." })
        : Results.NotFound(new { Error = $"Record {id} does not exist." }));

app.Run();
// 3.Models(Using C# Records for fast immutability)
// Make sure 'int Id' is the VERY FIRST parameter here:
public record BankRecord(int Id, string AccountNumber, string Type, decimal Balance);
public record CreateRecordDto(string AccountNumber, string Type, decimal Balance);