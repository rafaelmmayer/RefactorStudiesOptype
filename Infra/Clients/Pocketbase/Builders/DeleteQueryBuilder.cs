using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public class DeleteQueryBuilder<T> : QueryBuilderBase
    where T : class
{
    private readonly string _id;

    public DeleteQueryBuilder(PbClient client, string name, string id)
        : base(client, name) => _id = id;

    public async Task ExecuteAsync()
    {
        var req = new RestRequest(
            $"/api/collections/{CollectionName}/records/{_id}",
            Method.Delete
        );

        await Client.Rest.DeleteAsync(req);
    }
}
