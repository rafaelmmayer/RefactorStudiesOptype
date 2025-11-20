using Core.Domain.Studies.Results;
using Core.Domain.ValueObjects;

namespace Core.Domain.Studies;

public abstract class Study<TInputs, TOutputs>
    where TInputs : StudyInputs
    where TOutputs : StudyOutputs
{
    public abstract StudyType Type { get; }
    public StudyStatus Status { get; set; } = StudyStatus.Draft;
    
    public TInputs Inputs { get; }
    public TOutputs Outputs { get; }

    public Study(
        TInputs inputs,
        TOutputs outputs)
    {
        Inputs = inputs;
        Outputs = outputs;
    }

    public abstract ExportToExcelResults ExportToExcel();
}