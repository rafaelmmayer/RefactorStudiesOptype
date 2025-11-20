using Infra.Clients.Pocketbase.Attributes;

namespace Infra.Clients.Pocketbase.Models;

[Collection("discussions")]
public class DiscussionModel : BasePbModel
{
    public string Subject { get; set; } = string.Empty;
    public string[] ElementsId { get; set; } = [];
}