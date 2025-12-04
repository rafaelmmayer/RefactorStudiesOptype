using Infra.Clients.Pocketbase.Attributes;
using Newtonsoft.Json.Linq;

namespace Infra.Clients.Pocketbase.Models;

[Collection("study_types_settings")]
public class GlobalStudyAdvancedInputsModel : BasePbModel
{
    public string StudyType { get; set; } = string.Empty;
    public JObject Settings { get; set; } = null!;
    public string WorkspaceId { get; set; } = string.Empty;
}