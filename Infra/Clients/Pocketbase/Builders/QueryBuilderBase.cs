using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public abstract class QueryBuilderBase
{
    protected readonly PbClient Client;
    protected readonly string CollectionName;
    protected readonly Dictionary<string, string> Query = new();

    protected QueryBuilderBase(PbClient client, string collectionName)
    {
        Client = client;
        CollectionName = collectionName;
    }

    protected void AddQuery(string key, string value)
    {
        Query[key] = value;
    }

    protected void ApplyQuery(RestRequest req)
    {
        foreach (var kvp in Query)
        {
            req.AddQueryParameter(kvp.Key, kvp.Value);
        }
    }
}
