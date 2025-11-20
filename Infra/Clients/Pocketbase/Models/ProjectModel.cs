using Infra.Clients.Pocketbase.Attributes;

namespace Infra.Clients.Pocketbase.Models;

[Collection("projects")]
public class ProjectModel : BasePbModel
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}