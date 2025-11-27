using Core.Domain.Studies.Results;

namespace Core.Domain.Studies.Types.Concreto;

public class ConcretoStudyInputs : StudyInputs
{
    public string TipoDeBloco { get; set; } = string.Empty;
    
    public override ValidateResult Validate()
    {
        throw new NotImplementedException();
    }
}