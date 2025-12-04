using Application;
using Application.UseCases;
using Core.Domain.Studies.Types;
using Infra;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddApplication();
services.AddInfra();

var provider = services.BuildServiceProvider();

var process = provider.GetRequiredService<ProcessStudy>();
await process.Execute("02579415e04a474781cae7499b64ee8b");

// var createStudy = provider.GetRequiredService<CreateStudy>();
// await createStudy.Execute("a3dae775400e4689be24fa48cee1a306", StudyTypes.Alvenaria.TypeName);

Console.WriteLine();
