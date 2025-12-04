using Core.Domain.Studies.Export;

namespace Core.Domain.Studies.Types.Alvenaria;

public class AlvenariaStudyExport : IStudyExport<AlvenariaStudy>
{
    public string[] GetHeaders()
    {
        throw new NotImplementedException();
    }

    public object[] GetValues(AlvenariaStudy study)
    {
        var cost = study.CalculateCost();
        
        throw new NotImplementedException();
    }
}