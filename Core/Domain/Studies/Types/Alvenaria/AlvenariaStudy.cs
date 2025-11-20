using Core.Domain.Studies.Results;

namespace Core.Domain.Studies.Types.Alvenaria;

public class AlvenariaStudy : Study<AlvenariaStudyInputs, AlvenariaStudyOutputs>
{
    public override StudyType Type => StudyTypesList.Alvenaria;
    
    public AlvenariaStudy(AlvenariaStudyInputs inputs, AlvenariaStudyOutputs outputs) 
        : base(inputs, outputs)
    {
    }

    public override ExportToExcelResults ExportToExcel()
    {
        throw new NotImplementedException();
    }
}