namespace Core.Domain.Studies;

public record StudyRegistration(Type StudyType, Type InputsType, Type OutputsType);

public static class StudyRegistry
{
    private static readonly Dictionary<string, StudyRegistration> Map = new();

    static StudyRegistry()
    {
        var studyTypes = typeof(ICoreMarker)
            .Assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, BaseType: not null })
            .Where(t => t.BaseType!.IsGenericType)
            .Where(t => t.BaseType!.GetGenericTypeDefinition() == typeof(Study<,>));

        foreach (var t in studyTypes)
        {
            var baseGen = t.BaseType!;
            var args = baseGen.GetGenericArguments();

            var temp = (Study)Activator.CreateInstance(t)!;
            var key = temp.Type.TypeName;

            Map[key] = new StudyRegistration(StudyType: t, InputsType: args[0], OutputsType: args[1]);
        }
    }

    public static StudyRegistration GetStudyRegistration(string typeName) => Map[typeName];
}
