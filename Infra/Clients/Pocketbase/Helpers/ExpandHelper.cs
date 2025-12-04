using System.Linq.Expressions;

namespace Infra.Clients.Pocketbase.Helpers;

public static class ExpandHelper
{
    public static string GetExpandPath<T>(Expression<Func<T, object>> expr)
    {
        var body = expr.Body.ToString(); // ex: "x.CreatedBy"
        return body.Split('.').Last();
    }
}
