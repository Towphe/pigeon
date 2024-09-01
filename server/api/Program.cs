using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using repo;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// inject Environment Variables from appsetings.Environment.json
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.Environment.json", true)
       .AddEnvironmentVariables()
       .Build();

builder.Services.AddControllers();

// inject DB Context
builder.Services.AddDbContext<PostgresContext>(o => {
       o.UseNpgsql(builder.Configuration["DB_KEY"]);
});

// configure auth
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(o => {
       o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
       o.IncludeErrorDetails = true;
       o.SaveToken = true;
       o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
       {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(builder.Configuration["Supabase:JWT"])
              ),
              ValidateIssuer = false,
              ValidateAudience = true,
              ValidAudience = "authenticated",
              ValidIssuer = $"https://{builder.Configuration["Supabase:ProjectId"]}.supabase.co/auth/v1"
       };
       o.Events = new JwtBearerEvents();
});

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
