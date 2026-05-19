Here is the exact step-by-step process to get this Minimal API up and running on your machine from scratch using the .NET CLI.



**Step 1: Create the Project Folder and Web API Template**

Open your terminal (PowerShell, Command Prompt, or bash) and run the following commands to spin up a fresh, clean .NET Web API template:



\# Create a new directory for your API and enter it

mkdir LocalBankApi

cd LocalBankApi



\# Generate a fresh ASP.NET Core Minimal Web API project



dotnet new WebAPI



**Step 2: Open the Code in Your Editor**



Open this folder in your preferred integrated development environment (IDE). If you are using Visual Studio Code, you can launch it instantly from your terminal:



code .



Inside your project file explorer, locate and open the Program.cs file. This is the entry point where we will write our code.



**Step 3: Replace Program.cs with the Live API Code**

Select everything inside your current Program.cs file, delete it, and paste this streamlined, self-contained API that manages your 5 live records:



using System.Collections.Concurrent;



var builder = WebApplication.CreateBuilder(args);



// Add Swagger/OpenAPI documentation tools

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



var app = builder.Build();



// Enable the interactive Swagger web interface for testing

if (app.Environment.IsDevelopment())

{

&#x20;   app.UseSwagger();

&#x20;   app.UseSwaggerUI();

}



app.UseHttpsRedirection();



// 1. Seed your 5 records "on the fly" inside a thread-safe dictionary

var recordsDb = new ConcurrentDictionary<int, BankRecord>(new\[]

{

&#x20;   // Arguments: (Id, AccountNumber, Type, Balance)

&#x20;   new KeyValuePair<int, BankRecord>(1, new BankRecord(1, "ACC-9081", "Checking", 14500.23m)),

&#x20;   new KeyValuePair<int, BankRecord>(2, new BankRecord(2, "ACC-4322", "Savings", 250450.00m)),

&#x20;   new KeyValuePair<int, BankRecord>(3, new BankRecord(3, "LN-7761", "Auto Loan", -18200.50m)),

&#x20;   new KeyValuePair<int, BankRecord>(4, new BankRecord(4, "LN-1102", "Mortgage", -415000.00m)),

&#x20;   new KeyValuePair<int, BankRecord>(5, new BankRecord(5, "ACC-5541", "Investment", 89430.75m))

});



// 2. API Endpoints



// GET: Fetch all 5 records

app.MapGet("/api/records", () => Results.Ok(recordsDb.Values.OrderBy(r => r.Id)));



// GET: Fetch a single record by its specific ID

app.MapGet("/api/records/{id:int}", (int id) =>

&#x20;   recordsDb.TryGetValue(id, out var record) 

&#x20;       ? Results.Ok(record) 

&#x20;       : Results.NotFound(new { Error = $"Record {id} not found." }));



// POST: Add a new record on the fly

app.MapPost("/api/records", (CreateRecordDto input) =>

{

&#x20;   int newId = recordsDb.Keys.Count > 0 ? recordsDb.Keys.Max() + 1 : 1;

&#x20;   var newRecord = new BankRecord(newId, input.AccountNumber, input.Type, input.Balance);



&#x20;   if (recordsDb.TryAdd(newId, newRecord))

&#x20;   {

&#x20;       return Results.Created($"/api/records/{newId}", newRecord);

&#x20;   }

&#x20;   return Results.BadRequest(new { Error = "Failed to add record." });

});



// DELETE: Drop a record out of memory



app.MapDelete("/api/records/{id:int}", (int id) =>

&#x20;   recordsDb.TryRemove(id, out \_) 

&#x20;       ? Results.Ok(new { Status = $"Record {id} dropped successfully." }) 

&#x20;       : Results.NotFound(new { Error = $"Record {id} does not exist." }));



app.Run();



// 3. Models (Using C# Records for fast immutability)

public record BankRecord(int Id, string AccountNumber, string Type, decimal Balance);

public record CreateRecordDto(string AccountNumber, string Type, decimal Balance);

