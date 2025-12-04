using Core.Domain.Studies;
using Infra.Clients.Pocketbase;
using Infra.Clients.Pocketbase.Models;

namespace Application.UseCases;

public class CreateStudy
{
    private readonly PbClient _pbClient;

    public CreateStudy(PbClient pbClient)
    {
        _pbClient = pbClient;
    }

    public async Task Execute(string workspaceId, string studyType)
    {
        var study = StudyFactory.Create(studyType);
        
        var globalInputsModel = await _pbClient.Collection<GlobalStudyAdvancedInputsModel>()
            .List()
            .Filter($"workspaceId='{workspaceId}' && studyType='{studyType}'")
            .FirstOrDefaultAsync();

        if (globalInputsModel is null)
        {
            return;
        }

        var inputs = study.GetInputs();
        if (inputs is ISetAdvancedInputs setter)
        {
            var advType = StudyAdvancedInputsRegistry.GetAdvancedInputType(inputs.GetType());
            var obj = globalInputsModel.Settings.ToObject(advType);

            if (obj is StudyAdvancedInputs adv)
            {
                setter.SetAdvancedInputs(adv);
            }
        }
    }
}