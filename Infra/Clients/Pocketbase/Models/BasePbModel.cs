using Newtonsoft.Json.Linq;

namespace Infra.Clients.Pocketbase.Models;

public class BasePbModel
{
    public string Id { get; set; } = string.Empty;
    public string CollectionId { get; set; } = string.Empty;
    public string CollectionName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public Dictionary<string, JObject>? Expand { get; set; }
    
    protected T? Get<T>(string field) where T : BasePbModel
    {
        if (Expand is null)
        {
            return null;
        }
        
        return Expand.TryGetValue(field, out var value) ? value.ToObject<T>() : null;
    }
}