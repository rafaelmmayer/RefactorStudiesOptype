namespace Core.Domain.Studies.Export;

public static class StudyExportRegistry
{
    private static readonly Dictionary<Type, IStudyExport> Map = new();

    static StudyExportRegistry()
    {
        AutoRegisterExports();
    }

    private static void AutoRegisterExports()
    {
        var assembly = typeof(ICoreMarker).Assembly;

        var exportTypes = assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Select(t => new
            {
                Type = t,
                ExportInterface = t
                    .GetInterfaces()
                    .FirstOrDefault(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() ==
                        typeof(IStudyExport<>)
                    )
            })
            .Where(x => x.ExportInterface != null)
            .ToArray();

        foreach (var x in exportTypes)
        {
            var studyType = x.ExportInterface!.GetGenericArguments()[0];

            var instance = Activator.CreateInstance(x.Type)
                ?? throw new InvalidOperationException(
                    $"Não foi possível instanciar {x.Type.Name}"
                );

            var adapterType =
                typeof(StudyExportAdapter<>).MakeGenericType(studyType);

            var adapter = (IStudyExport)Activator.CreateInstance(
                adapterType,
                instance
            )!;

            Map[studyType] = adapter;
        }
    }

    public static void Register<TStudy>(IStudyExport<TStudy> export)
        where TStudy : Study
    {
        Map[typeof(TStudy)] =
            new StudyExportAdapter<TStudy>(export);
    }

    public static IStudyExport? GetExport(Type studyType)
    {
        return Map.GetValueOrDefault(studyType);
    }

    public static IStudyExport? GetExport(Study study)
        => GetExport(study.GetType());
}