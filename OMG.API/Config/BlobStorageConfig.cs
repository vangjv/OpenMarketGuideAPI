using OMG.Infrastructure.Services;

namespace OMG.API.Config
{
    public static class BlobStorageConfig
    {
        /// <summary>
        ///     Setup Blob Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void SetupBlobStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind database-related bindings
            var productImagesStorageConnectionString = configuration["productImagesStorageConnectionString"];
            var threeDModelsConnectionString = configuration["threeDModelsConnectionString"];

            services.AddSingleton<ProductImageStorageService, ProductImageStorageService>(serviceProvider =>
            {
                return new ProductImageStorageService(productImagesStorageConnectionString);
            });
            services.AddSingleton<ThreeDModelStorageService, ThreeDModelStorageService>(serviceProvider =>
            {
                return new ThreeDModelStorageService(threeDModelsConnectionString);
            });
        }
    }
}
