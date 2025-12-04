using Core.Domain.Studies.Export;
using Infra.Clients.Pocketbase;
using Infra.Clients.Pocketbase.Models;
using Infra.Converts;

namespace Application.UseCases;

public class ExportStudies
{
    private readonly PbClient _pbClient;

    public ExportStudies(PbClient pbClient)
    {
        _pbClient = pbClient;
    }
    
    public async Task Execute()
    {
        var studyModels = await _pbClient.Collection<StudyModel>()
            .FullList()
            .ExecuteAsync();

        var studies = studyModels
            .Select(PbStudyConverter.Convert)
            .ToArray();
        
        var typeGroups = studies
            .GroupBy(s => s.GetType())
            .ToArray();

        foreach (var type in typeGroups)
        {
            var export = StudyExportRegistry.GetExport(type.Key);

            if (export is null)
            {
                continue;
            }
            
            var headers = export.GetHeaders();

            foreach (var study in type)
            {
                var values = export.GetValues(study);
            }
        }
    }
}