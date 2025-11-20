using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Infra.Clients.Pocketbase;

public class PbClient
{
    public RestClient Rest { get; }

    public PbClient(HttpClient http, PbAuthenticator auth)
    {
        var options = new RestClientOptions
        {
            Authenticator = auth
        };
        
        Rest = new RestClient(
            http,
            options,
            configureSerialization: c => c.UseNewtonsoftJson()
        );
    }

    public PbCollection<T> Collection<T>() where T : class, new()
        => new PbCollection<T>(this);
}