using Core.Domain.Studies.Results;

namespace Core.Domain.Studies.Types.Alvenaria;

public class AlvenariaStudyInputs : StudyInputs
{
    public string TipoDeBloco { get; set; } = string.Empty;
    
    public override ValidateResult Validate()
    {
        throw new NotImplementedException();
    }
}