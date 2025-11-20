namespace Core.Domain.ValueObjects;

public readonly struct StudyStatus
{
    public static readonly StudyStatus Draft = new("draft");
    public static readonly StudyStatus Importing = new("importing");
    public static readonly StudyStatus InProgress = new("inprogress");
    public static readonly StudyStatus Complete = new("complete");
    public static readonly StudyStatus Canceled = new("canceled");

    public string Value { get; }

    private StudyStatus(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;
}