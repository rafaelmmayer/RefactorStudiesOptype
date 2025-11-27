using Application;
using Application.UseCases;
using Infra;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddApplication();
services.AddInfra();

var provider = services.BuildServiceProvider();

var useCase = provider.GetRequiredService<ChangeWorkspaceIds>();

await useCase.Execute();

Console.WriteLine();