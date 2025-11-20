using Infra.Clients.Pocketbase.Attributes;

namespace Infra.Clients.Pocketbase.Models;

[Collection("comments")]
public class CommentModel : BasePbModel
{
    public string Text { get; set; } = string.Empty;
    public string DiscussionId { get; set; } = string.Empty;
    public DiscussionModel? Discussion => Get<DiscussionModel>("discussionId");
    public string CreatedBy { get; set; } = string.Empty;
    public UserModel? CreatedByUser => Get<UserModel>("createdBy");
}