namespace Core.Domain.Studies.Types.Concreto;

public class ConcretoStudy : Study<ConcretoStudyInputs, ConcretoOutputs>
{
    public ConcretoStudy()
    {
        Type = StudyTypes.Concreto;
    }

    public override double CalculateCost()
    {
        throw new NotImplementedException();
    }
}
