using Azure.Identity;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Azure;
using OMG.API.Config;
using OMG.Infrastructure.Extensions;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var kvurl = configuration["KeyVaultURL"];
IConfiguration Configuration;
Configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", false, true)
     .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
         true)
     .AddCommandLine(args)
     .AddEnvironmentVariables()
     .AddAzureKeyVault(new Uri(kvurl), new DefaultAzureCredential())
     .Build();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});
// Add services to the container.
builder.Services.AddControllers(opts => opts.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Cosmos DB for application data
builder.Services.SetupCosmosDb(Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddHealthChecks();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
                   //.AllowCredentials();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI();
app.EnsureCosmosDbIsCreated();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
