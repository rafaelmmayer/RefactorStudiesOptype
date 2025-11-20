using Application;
using Application.UseCases;
using Infra;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddPocketBase();
services.AddApplication();

var provider = services.BuildServiceProvider();

var processStudy = provider.GetRequiredService<ProcessStudy>();

await processStudy.Execute("9dfb03494e984eef9801171ef48428ac");

Console.WriteLine();