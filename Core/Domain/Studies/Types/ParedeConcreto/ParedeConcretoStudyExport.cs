using Core.Domain.Studies.Export;

namespace Core.Domain.Studies.Types.ParedeConcreto;

public class ParedeConcretoStudyExport : IStudyExport<ParedeConcretoStudy>
{
    public string[] GetHeaders()
    {
        throw new NotImplementedException();
    }

    public object[] GetValues(ParedeConcretoStudy study)
    {
        throw new NotImplementedException();
    }
}