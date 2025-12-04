namespace Core.Domain.Studies;

public interface IStudyType
{
    public string TypeName { get; }
    public string DisplayName { get; }
    public string Descriptions { get; }
    public double Credits { get; }
    public string Image { get; }
    public TimeSpan TimeToComplete { get; }
}
