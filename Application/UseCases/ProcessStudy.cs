using Infra.Clients.Pocketbase;
using Infra.Clients.Pocketbase.Models;
using Infra.Factory;

namespace Application.UseCases;

public class ProcessStudy
{
    private readonly PbClient _pbClient;

    public ProcessStudy(
        PbClient pbClient)
    {
        _pbClient = pbClient;
    }

    public async Task Execute(string studyId)
    {
        var studyModel = await _pbClient.Collection<StudyModel>()
            .View(studyId)
            .ExecuteAsync();
        
        var study = StudyConverter.Convert(studyModel);

        study?.Inputs.Validate();
    }
}