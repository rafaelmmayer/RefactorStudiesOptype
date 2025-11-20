using Infra.Clients.Pocketbase.Builders;
using Infra.Clients.Pocketbase.Helpers;

namespace Infra.Clients.Pocketbase;

public class PbCollection<T> where T : class, new()
{
    private readonly PbClient _client;
    private readonly string _name;

    public PbCollection(PbClient client)
    {
        _client = client;
        _name = CollectionNameResolver.GetCollectionName<T>();
    }

    public ListQueryBuilder<T> List()
        => new ListQueryBuilder<T>(_client, _name);
    
    public FullListQueryBuilder<T> FullList()
        => new FullListQueryBuilder<T>(_client, _name);

    public ViewQueryBuilder<T> View(string id)
        => new ViewQueryBuilder<T>(_client, _name, id);

    public CreateQueryBuilder<T> Create(T data)
        => new CreateQueryBuilder<T>(_client, _name, data);

    public UpdateQueryBuilder<T> Update(string id, object data)
        => new UpdateQueryBuilder<T>(_client, _name, id, data);

    public DeleteQueryBuilder<T> Delete(string id)
        => new DeleteQueryBuilder<T>(_client, _name, id);
}