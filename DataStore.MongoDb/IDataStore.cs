namespace DataStore.MongoDb;

public interface IDataStore
{
    Task<KeyValue?> Get(string userId, string key);
    Task<KeyValue> Set(string userId, string key, string value);
    Task Delete(string userId, string key);
}
