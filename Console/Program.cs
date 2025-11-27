using Application;
using Application.UseCases;
using Infra;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddPocketBase();
services.AddApplication();

var provider = services.BuildServiceProvider();

var useCase = provider.GetRequiredService<ChangeWorkspaceIds>();

await useCase.Execute();

Console.WriteLine();