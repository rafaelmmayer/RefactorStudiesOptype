using System.Linq.Expressions;
using Infra.Clients.Pocketbase.Helpers;
using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public class UpdateQueryBuilder<T> : QueryBuilderBase where T : class
{
    private readonly string _id;
    private readonly object _data;

    public UpdateQueryBuilder(PbClient client, string name, string id, object data)
        : base(client, name)
    {
        _id = id;
        _data = data;
    }

    public UpdateQueryBuilder<T> Expand(string expand)
    {
        AddQuery("expand", expand);
        return this;
    }

    public async Task<T> ExecuteAsync()
    {
        var req = new RestRequest(
            $"/api/collections/{CollectionName}/records/{_id}",
            Method.Patch
        );

        ApplyQuery(req);
        req.AddJsonBody(_data);
        
        var res = await Client.Rest.PatchAsync<T>(req);
        
        if (res is null)
        {
            throw new Exception("Requisição falhou. Não foi possível obter o registro.");
        }

        return res;
    }
}