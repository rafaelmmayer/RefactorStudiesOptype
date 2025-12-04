namespace Core.Domain.Studies.Types.Fundacao;

public class FundacaoStudyType : IStudyType
{
    public string TypeName => "funcao";
    public string DisplayName { get; }
    public string Descriptions { get; }
    public double Credits { get; }
    public string Image { get; }
    public TimeSpan TimeToComplete { get; }
}