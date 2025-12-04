namespace Core.Domain.Studies.Types.Fundacao;

public class FundacaoStudy : Study<FundacaoStudyInputs, FundacaoOutputs>
{
    public FundacaoStudy()
    {
        Type = StudyTypes.Fundacao;
    }
    
    public override double CalculateCost()
    {
        throw new NotImplementedException();
    }
}