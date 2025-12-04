namespace Core.Domain.Studies;

public record AdvancedInputsRegistration(Type InputsType, Type AdvancedInputsType);

public static class StudyAdvancedInputsRegistry
{
    private static readonly Dictionary<Type, AdvancedInputsRegistration> Map = new();

    static StudyAdvancedInputsRegistry()
    {
        var types = typeof(ICoreMarker)
            .Assembly.GetTypes()
            .Where(t => t is { IsAbstract: false })
            .Where(t => t.BaseType != null)
            .Where(t =>
                t.BaseType!.IsGenericType &&
                t.BaseType!.GetGenericTypeDefinition() == typeof(StudyInputs<>)
            );

        foreach (var t in types)
        {
            var advType = t.BaseType!.GetGenericArguments()[0];
            Map[t] = new AdvancedInputsRegistration(t, advType);
        }
    }

    public static Type GetAdvancedInputType(Type inputsType)
        => Map[inputsType].AdvancedInputsType;
}