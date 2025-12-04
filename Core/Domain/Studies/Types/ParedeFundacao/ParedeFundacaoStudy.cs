namespace Core.Domain.Studies.Types.ParedeFundacao;

public class ParedeFundacaoStudy : Study<ParedeFundacaoInputs, ParedeFundacaoOutputs>
{
    public ParedeFundacaoStudy()
    {
        Type = StudyTypes.ParedeFundacao;
    }

    public override double CalculateCost()
    {
        throw new NotImplementedException();
    }
}