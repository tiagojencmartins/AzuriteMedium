using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class ServiceDependencies
{
    public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(provider => 
            new BlobContainerClient(
                configuration.GetValue<string>("Azurite:ConnectionString"),
                configuration.GetValue<string>("Azurite:Container")));
                
        services.AddSingleton<IBlobService, BlobService>();
    }
}