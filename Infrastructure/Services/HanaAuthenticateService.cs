using System.Net.Http.Json;
using System.Text.Json;
using Application.DTOs;
using Application.Models;
using Application.Services.Interfaces;

namespace Application.Services;

public class HanaAuthenticateService : IHanaAuthenticateService
{
    private const string ApiEndpoint = "/b1s/v1/Login";
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ITokenManagementService _tokenManagementService;
    private readonly JsonSerializerOptions _options;


    public HanaAuthenticateService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _httpClient = _clientFactory.CreateClient("ServiceLayer");
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<string> Authenticate(AuthenticateHanaDTO data)
    {
        
        using (var response = await _httpClient.PostAsJsonAsync(ApiEndpoint, data, _options))
        {
            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content.ReadFromJsonAsync<HanaSessionDTO>();
                return resultData.SessionId;
            }

            var errorData = await response.Content.ReadFromJsonAsync<ServiceLayerResponse.Fail>();
            throw new Exception(errorData.Error.Message.Value);
        }
    }
}