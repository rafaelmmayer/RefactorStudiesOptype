using Core.Domain.Studies;
using Core.Domain.Studies.Export;
using Core.Domain.Studies.Types.Alvenaria;
using Infra.Clients.Pocketbase;
using Infra.Clients.Pocketbase.Models;
using Infra.Converts;

namespace Application.UseCases;

public class ProcessStudy
{
    private readonly PbClient _pbClient;

    public ProcessStudy(PbClient pbClient)
    {
        _pbClient = pbClient;
    }

    public async Task Execute(string studyId)
    {
        var studyModel = await _pbClient.Collection<StudyModel>()
            .View(studyId)
            .ExecuteAsync();
        
        var study = PbStudyConverter.Convert(studyModel);
        
        var validate = study.ValidateInputs();
            
        if (validate.IsValid)
        {
        }
    }
}
