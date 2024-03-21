using System.Text.Json.Serialization;
using WorkTracker.API.Setup;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WorkTracker.Gateways.MySQL.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new DashRouteConvention());
    }).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configure Swagger options
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Work Tracker API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddClockServices();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var catalogContext = serviceScope.ServiceProvider.GetService<ClockContext>();
    catalogContext!.Database.Migrate();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Type", "application/json");
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    await next.Invoke();
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
