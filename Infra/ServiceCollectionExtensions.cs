using Infra.Clients.Pocketbase;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class ServiceCollectionExtensions
{
    public static void AddPocketBase(this IServiceCollection services)
    {
        services.AddHttpClient("pb", client =>
        {
            client.BaseAddress = new Uri("https://pb-optype.mayerafa.com");
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddSingleton<PbTokenService>(sp =>
        {
            var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("pb");
            return new PbTokenService(http);
        });

        services.AddSingleton<PbAuthenticator>();

        services.AddSingleton<PbClient>(sp =>
        {
            var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("pb");
            var auth = sp.GetRequiredService<PbAuthenticator>();
            
            return new PbClient(http, auth);
        });
    }
}