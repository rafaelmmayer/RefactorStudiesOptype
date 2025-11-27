using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Infra.Clients.Pocketbase;

public class PbTokenService
{
    private readonly RestClient _rest;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _token;
    private DateTime _expiresAt;

    public PbTokenService(HttpClient httpClient)
    {
        _rest = new RestClient(httpClient, configureSerialization: c => c.UseNewtonsoftJson());
    }

    public async Task<string> GetToken(CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            if (_token != null && DateTime.UtcNow < _expiresAt)
                return _token;

            var req = new RestRequest("/api/collections/_superusers/auth-with-password");
            
            req.AddBody(new { identity = "inovacao@optype.com.br", password = "Optype_2025" });

            var res = await _rest.PostAsync<JObject>(req, ct);

            var token = res?["token"]?.ToString() ?? throw new Exception("Falha ao obter token.");

            _token = token;
            _expiresAt = DateTime.UtcNow.AddHours(1);

            return token;
        }
        finally
        {
            _lock.Release();
        }
    }
}