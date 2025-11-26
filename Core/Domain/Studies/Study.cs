using Core.Domain.Studies.Results;
using Core.Domain.ValueObjects;

namespace Core.Domain.Studies;

public abstract class Study
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public abstract StudyType Type { get; }
    public StudyStatus Status { get; set; } = StudyStatus.Draft;
    
    public StudyInputs Inputs { get; init; } = null!;
    public StudyOutputs? Outputs { get; init; }
    
    public abstract ExportToExcelResults ExportToExcel();
}