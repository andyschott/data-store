using System.ComponentModel.DataAnnotations;
using DataStore.MongoDb;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceConfiguration
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services,
        MongoDbConfig config)
    {
        Validator.ValidateObject(config, new ValidationContext(config));

        services.AddScoped(_ => new MongoClient(config.ConnectionString));

        services.AddScoped<IDataStore, MongoDbDataStore>(sp => 
            ActivatorUtilities.CreateInstance<MongoDbDataStore>(sp, config.DatabaseName!));

        return services;        
    }
}
