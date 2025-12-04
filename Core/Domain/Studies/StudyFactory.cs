namespace Core.Domain.Studies;

public static class StudyFactory
{
    public static TStudy Create<TStudy>()
        where TStudy : Study, new()
    {
        return new TStudy();
    }

    public static Study Create(string studyTypeName)
    {
        var reg = StudyRegistry.GetStudyRegistration(studyTypeName);
        return (Study)Activator.CreateInstance(reg.StudyType)!;
    }
}
