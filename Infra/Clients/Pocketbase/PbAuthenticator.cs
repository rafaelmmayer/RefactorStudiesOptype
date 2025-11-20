using RestSharp;
using RestSharp.Authenticators;

namespace Infra.Clients.Pocketbase;

public class PbAuthenticator : IAuthenticator
{
    private readonly PbTokenService _tokenService;

    public PbAuthenticator(PbTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        var token = await _tokenService.GetToken();
        request.AddHeader("Authorization", $"Bearer {token}");
    }
}