using Core.Domain.Studies.Results;

namespace Core.Domain.Studies.Types.Alvenaria;

[StudyDefinition(typeof(AlvenariaStudyInputs), typeof(AlvenariaStudyOutputs))]
public class AlvenariaStudy : Study
{
    public override StudyType Type => StudyTypes.Alvenaria;

    public override ExportToExcelResults ExportToExcel()
    {
        throw new NotImplementedException();
    }
}