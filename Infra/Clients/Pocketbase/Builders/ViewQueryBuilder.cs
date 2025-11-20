using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public class ViewQueryBuilder<T> : QueryBuilderBase where T : class
{
    private readonly string _id;

    public ViewQueryBuilder(PbClient client, string name, string id)
        : base(client, name) => _id = id;

    public ViewQueryBuilder<T> Expand(string expand)
    {
        AddQuery("expand", expand);
        return this;
    }

    public async Task<T> ExecuteAsync()
    {
        var req = new RestRequest(
            $"/api/collections/{CollectionName}/records/{_id}"
        );

        ApplyQuery(req);
        
        var res = await Client.Rest.GetAsync<T>(req);

        if (res is null)
        {
            throw new Exception("Requisição falhou. Não foi possível obter o registro.");
        }

        return res;
    }
}