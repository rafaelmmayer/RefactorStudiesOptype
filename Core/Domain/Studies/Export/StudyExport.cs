namespace Core.Domain.Studies.Export;

public interface IStudyExport<in T> where T : Study
{
    string[] GetHeaders();
    object[] GetValues(T study);
}

public interface IStudyExport
{
    string[] GetHeaders();
    object[] GetValues(Study study);
}

public class StudyExportAdapter<TStudy> : IStudyExport
    where TStudy : Study
{
    private readonly IStudyExport<TStudy> _inner;

    public StudyExportAdapter(IStudyExport<TStudy> inner)
    {
        _inner = inner;
    }

    public string[] GetHeaders() => _inner.GetHeaders();

    public object[] GetValues(Study study)
    {
        return _inner.GetValues((TStudy)study); 
    }
}