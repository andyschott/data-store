using DataStore.MongoDb;
using Microsoft.AspNetCore.Mvc;

namespace data_store;

[Route("api/values/{userId}")]
public class ValuesController : Controller
{
    private readonly IDataStore _dataStore;

    public ValuesController(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    [HttpGet("{key}")]
    public async Task<ActionResult<string>> Get(string userId, string key)
    {
        var item = await _dataStore.Get(userId, key);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(new
        {
            Value = item.Value,
        });
    }

    [HttpPut]
    public async Task<ActionResult> Create(string userId,
        [FromBody] UpdateItem item)
    {
        await _dataStore.Set(userId, item.Key, item.Value);
        return Ok();
    }

    [HttpDelete("{key}")]
    public async Task<ActionResult> Delete(string userId, string key)
    {
        await _dataStore.Delete(userId, key);
        return Ok();
    }
}
