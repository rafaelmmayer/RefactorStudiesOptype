namespace Core.Domain.Studies;

public abstract class StudyType
{
    public abstract string Type { get; }
    public abstract string DisplayName { get; }
    public abstract string Descriptions { get; }
    public abstract double Credits { get; }
    public abstract string Image { get; }
    public abstract TimeSpan TimeToComplete { get; }
}