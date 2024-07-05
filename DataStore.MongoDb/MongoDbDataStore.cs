using MongoDB.Driver;

namespace DataStore.MongoDb;

public class MongoDbDataStore : IDataStore
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoDbDataStore(MongoClient client, string databaseName)
    {
        _client = client;
        _database = _client.GetDatabase(databaseName);
    }

    public async Task<KeyValue?> Get(string userId, string key)
    {
        var values = _database.GetCollection<KeyValue>(userId);
        var item = await values.Find(item => item.Key == key)
            .FirstOrDefaultAsync();

        return item;
    }

    public async Task<KeyValue> Set(string userId, string key, string value)
    {
        var values = _database.GetCollection<KeyValue>(userId);

        var item = await values.Find(item => item.Key == key)
            .FirstOrDefaultAsync();
        if (item is null)
        {
            item = new KeyValue
            {
                UserId = userId,
                Key = key,
                Value = value
            };
            await values.InsertOneAsync(item);
        }
        else 
        {
            item.Value = value;
            await values.ReplaceOneAsync(item => item.Key == key,
                item);
        }

        return item;
    }

    public async Task Delete(string userId, string key)
    {
        var values = _database.GetCollection<KeyValue>(userId);

        var item = await values.Find(item => item.Key == key)
            .FirstOrDefaultAsync();

        if (item is null)
        {
            return;
        }

        await values.DeleteOneAsync(item => item.Key == key);
    }
}
