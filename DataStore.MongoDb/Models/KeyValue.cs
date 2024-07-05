using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataStore.MongoDb;

public class KeyValue
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string UserId { get; set; } = null!;

    [BsonElement("Name")]
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;
}
