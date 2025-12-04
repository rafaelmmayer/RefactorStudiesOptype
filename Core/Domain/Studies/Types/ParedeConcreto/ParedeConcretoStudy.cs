namespace Core.Domain.Studies.Types.ParedeConcreto;

public class ParedeConcretoStudy : Study<ParedeConcretoInputs, ParedeConcretoOutputs>
{
    public ParedeConcretoStudy()
    {
        Type = StudyTypes.ParedeConcreto;
    }

    public override double CalculateCost()
    {
        throw new NotImplementedException();
    }
}