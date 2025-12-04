using Core.Domain.Studies;
using Core.Domain.ValueObjects;
using Infra.Clients.Pocketbase.Models;

namespace Infra.Converts;

public static class PbStudyConverter
{
    public static T Convert<T>(StudyModel model) where T : Study
    {
        if (Convert(model) is not T study)
        {
            throw new InvalidCastException();
        }

        return study;
    }
    
    public static Study Convert(StudyModel model)
    {
        var reg = StudyRegistry.GetStudyRegistration(model.Type);

        var study = StudyFactory.Create(model.Type);

        if (model.Inputs?.ToObject(reg.InputsType) is StudyInputs inputs)
        {
            study.SetInputs(inputs);
        }
        
        if (model.Outputs?.ToObject(reg.OutputsType) is StudyOutputs outputs)
        {
            study.SetOutputs(outputs);
        }

        study.Name = model.Name;
        study.Code = model.Code;
        study.Description = model.Description;
        study.Status = new StudyStatus(model.Status);

        return study;
    }
}
