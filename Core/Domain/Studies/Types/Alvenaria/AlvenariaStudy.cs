namespace Core.Domain.Studies.Types.Alvenaria;

public class AlvenariaStudy : Study<AlvenariaStudyInputs, AlvenariaOutputs>
{
    public AlvenariaStudy()
    {
        Type = StudyTypes.Alvenaria;
    }

    public override double CalculateCost()
    {
        throw new NotImplementedException();
    }
}
