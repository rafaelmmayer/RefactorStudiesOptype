using Core.Domain.Studies;
using Core.Domain.Studies.Types;
using Core.Domain.Studies.Types.Alvenaria;
using Core.Domain.Studies.Types.Concreto;
using Core.Domain.ValueObjects;
using Infra.Clients.Pocketbase.Models;

namespace Infra.Factory;

public static class StudyConverterGenerated
{
    public static Study Convert(StudyModel model)
    {
        Study study;

        // 1. Cria a instância correta
        if (model.Type == StudyTypes.Alvenaria.TypeName)
        {
            var inputs = model.Inputs.ToObject<AlvenariaStudyInputs>()!;
            var outputs = model.Outputs?.ToObject<AlvenariaStudyOutputs>();
            study = new AlvenariaStudy
            {
                Inputs = inputs,
                Outputs = outputs
            };
        }
        else if (model.Type == StudyTypes.Concreto.TypeName)
        {
            var inputs = model.Inputs.ToObject<ConcretoStudyInputs>()!;
            var outputs = model.Outputs?.ToObject<ConcretoStudyOutputs>();
            study = new ConcretoStudy
            {
                Inputs = inputs,
                Outputs = outputs
            };
        }
        else
        {
            throw new Exception("Tipo de estudo não conhecido");
        }
        
        return study;
    }
}

public static class StudyConverter
{
    public static Study Convert(StudyModel model)
    {
        var study = StudyConverterGenerated.Convert(model);

        study.Name = model.Name;
        study.Code = model.Code;
        study.Description = model.Description;
        study.Status = new StudyStatus(model.Status);

        return study;
    }
}