using Infra.Clients.Pocketbase.Attributes;
using Newtonsoft.Json.Linq;

namespace Infra.Clients.Pocketbase.Models;

[Collection("studies")]
public class StudyModel : BasePbModel
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public JObject Inputs { get; set; } = null!;
    public JObject? Outputs { get; set; }

    public ProjectModel? Project => Get<ProjectModel>("projectId");
}