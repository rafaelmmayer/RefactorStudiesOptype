namespace Infra.Clients.Pocketbase.Results;

public class ListResult<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public List<T> Items { get; set; } = [];
}
