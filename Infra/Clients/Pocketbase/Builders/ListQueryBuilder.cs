using System.Linq.Expressions;
using Infra.Clients.Pocketbase.Helpers;
using Infra.Clients.Pocketbase.Results;
using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public class ListQueryBuilder<T> : QueryBuilderBase
    where T : class
{
    public ListQueryBuilder(PbClient client, string name)
        : base(client, name) { }

    public ListQueryBuilder<T> Page(int p)
    {
        AddQuery("page", p.ToString());
        return this;
    }

    public ListQueryBuilder<T> PerPage(int pp)
    {
        AddQuery("perPage", pp.ToString());
        return this;
    }

    public ListQueryBuilder<T> Sort(string sort)
    {
        AddQuery("sort", sort);
        return this;
    }

    public ListQueryBuilder<T> Filter(string filter)
    {
        AddQuery("filter", filter);
        return this;
    }

    public ListQueryBuilder<T> Expand(string expand)
    {
        AddQuery("expand", expand);
        return this;
    }

    public async Task<ListResult<T>> ExecuteAsync()
    {
        var req = new RestRequest($"/api/collections/{CollectionName}/records");

        ApplyQuery(req);

        var res = await Client.Rest.GetAsync<ListResult<T>>(req);

        if (res is null)
        {
            throw new Exception("Requisição falhou. Não foi possível obter o registro.");
        }

        return res;
    }

    public async Task<T?> FirstOrDefaultAsync()
    {
        AddQuery("perPage", "1");

        AddQuery("skipTotal", "true");

        var req = new RestRequest($"/api/collections/{CollectionName}/records");

        ApplyQuery(req);

        var result = await Client.Rest.GetAsync<ListResult<T>>(req);

        if (result?.Items == null || result.Items.Count == 0)
            return null;

        return result.Items.First();
    }
}
