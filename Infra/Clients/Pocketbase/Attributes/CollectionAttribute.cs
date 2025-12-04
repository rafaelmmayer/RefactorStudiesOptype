namespace Infra.Clients.Pocketbase.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CollectionAttribute : Attribute
{
    public string Name { get; }

    public CollectionAttribute(string name)
    {
        Name = name;
    }
}
