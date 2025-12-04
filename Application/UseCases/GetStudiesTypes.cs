using Core.Domain.Studies;
using Core.Domain.Studies.Types;

namespace Application.UseCases;

public class GetStudiesTypes
{
    public Task<IStudyType[]> Execute()
    {
        return Task.FromResult(StudyTypes.List);
    }
}