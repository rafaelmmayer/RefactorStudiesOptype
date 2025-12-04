using System.Linq.Expressions;
using Infra.Clients.Pocketbase.Helpers;
using Infra.Clients.Pocketbase.Results;
using RestSharp;

namespace Infra.Clients.Pocketbase.Builders;

public class FullListQueryBuilder<T> : QueryBuilderBase
    where T : class
{
    public FullListQueryBuilder(PbClient client, string name)
        : base(client, name)
    {
        // skipTotal = true → mais rápido
        AddQuery("skipTotal", "true");
    }

    public FullListQueryBuilder<T> Filter(string filter)
    {
        AddQuery("filter", filter);
        return this;
    }

    public FullListQueryBuilder<T> Sort(string sort)
    {
        AddQuery("sort", sort);
        return this;
    }

    public FullListQueryBuilder<T> Expand(string expand)
    {
        AddQuery("expand", expand);
        return this;
    }

    public async Task<List<T>> ExecuteAsync()
    {
        var allItems = new List<T>();
        var page = 1;

        while (true)
        {
            var req = new RestRequest($"/api/collections/{CollectionName}/records");

            // paginação interna automática
            req.AddQueryParameter("page", page.ToString());
            req.AddQueryParameter("perPage", "100"); // 100 é limite do PB

            ApplyQuery(req);

            var result = await Client.Rest.GetAsync<ListResult<T>>(req);

            if (result?.Items == null || result.Items.Count == 0)
                break;

            allItems.AddRange(result.Items);

            if (result.Items.Count < 100)
                break; // acabou

            page++;
        }

        return allItems;
    }
}
