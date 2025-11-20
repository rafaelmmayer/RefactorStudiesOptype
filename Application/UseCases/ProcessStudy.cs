using Core.Domain.Studies;
using Core.Domain.Studies.Types;
using Core.Domain.Studies.Types.Alvenaria;
using Infra.Clients.Pocketbase;
using Infra.Clients.Pocketbase.Models;

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
        var study = await _pbClient.Collection<StudyModel>()
            .View(studyId)
            .ExecuteAsync();
        
        var validator = GetValidator(study);

        validator?.Validate();
    }
    
    private IValidator? GetValidator(StudyModel studyModel)
    {
        IValidator? validator;
        if (studyModel.Type == StudyTypesList.Concreto.Type)
        {
            validator = studyModel.Inputs.ToObject<AlvenariaStudyInputs>();
        }
        else
        {
            validator = null;
        }

        return validator;
    }
}