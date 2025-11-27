using Core.Domain.Studies.Results;

namespace Core.Domain.Studies.Types.Concreto;

[StudyDefinition(typeof(ConcretoStudyInputs), typeof(ConcretoStudyOutputs))]
public class ConcretoStudy : Study
{
    public override StudyType Type => StudyTypes.Concreto;

    public override ExportToExcelResults ExportToExcel()
    {
        throw new NotImplementedException();
    }
}