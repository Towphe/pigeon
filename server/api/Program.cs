using Microsoft.EntityFrameworkCore;
using repo;

var builder = WebApplication.CreateBuilder(args);

// inject Environment Variables from appsetings.Environment.json
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.Environment.json", true)
       .AddEnvironmentVariables()
       .Build();

// inject DB Context
builder.Services.AddDbContext<PostgresContext>(o => {
       o.UseNpgsql(builder.Configuration["DB_KEY"]);
});

var app = builder.Build();

app.MapGet("/", () => "Hello, world!");

app.Run();
