using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.Infrastructure.CosmosDbData;

var host = new HostBuilder()
.ConfigureFunctionsWorkerDefaults()
.ConfigureAppConfiguration(builder =>
{
    builder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

    var config = builder.Build();
})
.Build();

host.Run();


internal static class WorkerConfigurationExtensions
{
    /// <summary>
    /// Calling ConfigureFunctionsWorkerDefaults() configures the Functions Worker to use System.Text.Json for all JSON
    /// serialization and sets JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    /// This method uses DI to modify the JsonSerializerOptions. Call /api/HttpFunction to see the changes.
    /// </summary>
    public static IFunctionsWorkerApplicationBuilder ConfigureSystemTextJson(this IFunctionsWorkerApplicationBuilder builder)
    {
        builder.Services.Configure<JsonSerializerOptions>(jsonSerializerOptions =>
        {
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            // override the default value
            jsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });

        return builder;
    }

    /// <summary>
    /// The functions worker uses the Azure SDK's ObjectSerializer to abstract away all JSON serialization. This allows you to
    /// swap out the default System.Text.Json implementation for the Newtonsoft.Json implementation.
    /// To do so, add the Microsoft.Azure.Core.NewtonsoftJson nuget package and then update the WorkerOptions.Serializer property.
    /// This method updates the Serializer to use Newtonsoft.Json. Call /api/HttpFunction to see the changes.
    /// </summary>
    public static IFunctionsWorkerApplicationBuilder UseNewtonsoftJson(this IFunctionsWorkerApplicationBuilder builder)
    {
        builder.Services.Configure<WorkerOptions>(workerOptions =>
        {
            var settings = NewtonsoftJsonObjectSerializer.CreateJsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.MaxDepth = 1;
            workerOptions.Serializer = new NewtonsoftJsonObjectSerializer(settings);
        });

        return builder;
    }

    /// <summary>
    ///     Ensure Cosmos DB is created
    /// </summary>
    /// <param name="builder"></param>
    //public static IFunctionsWorkerApplicationBuilder EnsureCosmosDbIsCreated(this IFunctionsWorkerApplicationBuilder builder)
    //{
    //    {
    //        string cosmosClient = System.Environment.GetEnvironmentVariable("dbConnString", EnvironmentVariableTarget.Process);
    //        ICosmosDbContainerFactory factory = new CosmosDbContainerFactory();

    //        factory.EnsureDbSetupAsync().Wait();
    //        return builder;
    //    }
    //}
}
