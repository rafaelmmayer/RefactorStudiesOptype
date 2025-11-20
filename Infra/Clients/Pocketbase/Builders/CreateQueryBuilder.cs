using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public class CreateQueryBuilder<T> : QueryBuilderBase where T : class
{
    private readonly T _data;

    public CreateQueryBuilder(PbClient client, string name, T data)
        : base(client, name) => _data = data;
    
    public CreateQueryBuilder<T> Expand(string expand)
    {
        AddQuery("expand", expand);
        return this;
    }

    public async Task<T> ExecuteAsync()
    {
        var req = new RestRequest(
            $"/api/collections/{CollectionName}/records",
            Method.Post
        );

        ApplyQuery(req);
        
        req.AddJsonBody(_data);

        var res = await Client.Rest.PostAsync<T>(req);
        
        if (res is null)
        {
            throw new Exception("Requisição falhou. Não foi possível obter o registro.");
        }

        return res;
    }
}