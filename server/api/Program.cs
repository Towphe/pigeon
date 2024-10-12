using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using repo;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Auth0.AspNetCore.Authentication;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using services.account;
using api.Filters;

var builder = WebApplication.CreateBuilder(args);

// inject Environment Variables from appsetings.Environment.json
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.Environment.json", true)
       .AddEnvironmentVariables()
       .Build();

builder.Services.AddControllers()
       .AddJsonOptions(o => {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
                o.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
       });

builder.WebHost.ConfigureKestrel(opts => {
       opts.AddServerHeader = false;
});

builder.Services.AddAuthentication(opts => {
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts => {
    // these 2 values are critical
    opts.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    opts.Audience = $"{builder.Configuration["Auth0:Audience"]}/";
    opts.TokenValidationParameters = new TokenValidationParameters {
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
    opts.RequireHttpsMetadata = false;  // change this later
    opts.Events = new JwtBearerEvents {
        OnTokenValidated = context => {
            // If you need the user's information for any reason at this point, you can get it by looking at the Claims property
            // of context.Ticket.Principal.Identity
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // Get the user's ID
                string userId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                // Get the name
                string name = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            }

            return Task.FromResult(0);
        }
    };
})
.AddCookie(o =>
    {
        o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        o.Cookie.SameSite = SameSiteMode.Strict;
        o.Cookie.HttpOnly = true;
});

builder.Services.AddAuthorization(opts => {
    // policies/claims goes here
    // opts.AddPolicy("read:user-profiles", policy => policy.Requirements.Add(new HasScopeRequirement("read:user-profiles", builder.Configuration["Auth0:Domain"])));
});

// inject DB Context
builder.Services.AddDbContext<PigeonContext>(o => {
       o.UseNpgsql(builder.Configuration["DB_KEY"]);
});

// documentation generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts => {
    opts.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Elbilihan API",
        Description = "Elbilihan API",
        Version = "v1"
    });
});

builder.Services.AddCors(opts => {
    opts.AddPolicy(
        "_allowedOrigins",
        policy => {
            policy.WithOrigins("https://localhost:44350")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        }
    );
});

// custom services
builder.Services.AddScoped<IAccountHandler, AccountHandler>();
builder.Services.AddScoped<Auth0IPFilter>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});  

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
       app.UseCors("_allowedOrigins");
}

app.UseAuthentication();

app.UseAuthorization();
// app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "custome",
    pattern: "v1/{controller=Home}/{action=Index}/{id?}"
);

app.Run();
