using System.ComponentModel.DataAnnotations;

namespace DataStore.MongoDb;

public class MongoDbConfig
{
    [Required]
    public string? ConnectionString { get; set; }
    [Required]
    public string? DatabaseName { get; set; }
}
