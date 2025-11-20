namespace Core.Domain.Studies.Results;

public class ExportToExcelResults
{
    public StudyType StudyType { get; }
    public object[] ColumnsValues { get; }
    
    public ExportToExcelResults(
        StudyType studyType, 
        object[] columnsValues)
    {
        StudyType = studyType;
        ColumnsValues = columnsValues;
    }
}