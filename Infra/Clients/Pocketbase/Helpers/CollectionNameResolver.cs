using Infra.Clients.Pocketbase.Attributes;

namespace Infra.Clients.Pocketbase.Helpers;

public static class CollectionNameResolver
{
    public static string GetCollectionName<T>()
    {
        var type = typeof(T);

        var attributes = type.GetCustomAttributes(typeof(CollectionAttribute), false);

        if (attributes.FirstOrDefault() is CollectionAttribute attr)
        {
            return attr.Name;
        }

        // fallback: usar o nome da classe em lowercase
        return type.Name.ToLower();
    }
}
