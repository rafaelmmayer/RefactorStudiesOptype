using Infra.Clients.Pocketbase;
using Infra.Clients.Pocketbase.Models;

namespace Application.UseCases;

public class ChangeWorkspaceIds
{
    private readonly PbClient _pbClient;

    public ChangeWorkspaceIds(PbClient pbClient)
    {
        _pbClient = pbClient;
    }
    
    public async Task Execute()
    {
        var projects = await _pbClient.Collection<ProjectModel>()
            .FullList()
            .ExecuteAsync();
        
        var studies = await _pbClient.Collection<StudyModel>()
            .FullList()
            .ExecuteAsync();

        var update = new { workspaceId = "c879082938114c33b88c940272b738dc" };
        
        foreach (var p in projects)
        {
            await _pbClient.Collection<ProjectModel>()
                .Update(p.Id, update)
                .ExecuteAsync();
        }
        
        foreach (var s in studies)
        {
            await _pbClient.Collection<StudyModel>()
                .Update(s.Id, update)
                .ExecuteAsync();
        }
    }
}