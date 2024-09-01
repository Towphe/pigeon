var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.Environment.json", true)
       .AddEnvironmentVariables()
       .Build();     

var app = builder.Build();

app.MapGet("/", () => "Hello, world!");

app.Run();
