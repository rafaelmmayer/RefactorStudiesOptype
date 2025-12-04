using Infra.Clients.Pocketbase.Attributes;

namespace Infra.Clients.Pocketbase.Models;

[Collection("users")]
public class UserModel : BasePbModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
