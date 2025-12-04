using Core.Domain.Studies.Validator;
using Core.Domain.ValueObjects;
using FluentValidation.Results;

namespace Core.Domain.Studies;

public abstract class Study
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IStudyType Type { get; protected init; } = null!;
    public StudyStatus Status { get; set; } = StudyStatus.Draft;

    public abstract StudyInputs GetInputs();
    public abstract void SetInputs(StudyInputs inputs);

    public abstract StudyOutputs GetOutputs();
    public abstract void SetOutputs(StudyOutputs outputs);
    
    public abstract double CalculateCost();
    
    public ValidationResult ValidateInputs()
    {
        var inputs = GetInputs();
        var fn = StudyValidatorRegistry.GetValidator(inputs.GetType());
        var res = fn(inputs);

        return res;
    }
}

public abstract class Study<TInputs, TOutputs> : Study
    where TInputs : StudyInputs, new()
    where TOutputs : StudyOutputs, new()
{
    public TInputs Inputs { get; private set; } = new();
    public TOutputs Outputs { get; private set; } = new();

    public override StudyInputs GetInputs() => Inputs;

    public override void SetInputs(StudyInputs inputs)
    {
        if (inputs is not TInputs typed)
        {
            throw new ArgumentException(
                $"Tipo inválido para SetInputs. Esperado: {typeof(TInputs).Name}, "
                    + $"recebido: {inputs.GetType().Name}."
            );
        }

        Inputs = typed;
    }
    
    public override StudyOutputs GetOutputs() => Outputs;

    public override void SetOutputs(StudyOutputs outputs)
    {
        if (outputs is not TOutputs typed)
        {
            throw new ArgumentException(
                $"Tipo inválido para SetInputs. Esperado: {typeof(TOutputs).Name}, "
                + $"recebido: {outputs.GetType().Name}."
            );
        }

        Outputs = typed;
    }
}
