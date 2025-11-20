using Core.Domain.Studies.Results;

namespace Core.Domain.Studies;

public interface IValidator
{
    ValidateResult Validate();
}

public abstract class StudyInputs : IValidator
{
    public abstract ValidateResult Validate();
}