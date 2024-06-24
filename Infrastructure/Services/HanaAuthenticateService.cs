using System.Net.Http.Json;
using System.Text.Json;
using Application.DTOs;
using Application.Models;
using Application.Services.Interfaces;

namespace Application.Services;

public class HanaAuthenticateService : IHanaAuthenticateService
{
    private const string ApiEndpoint = "/Login";
    private readonly IHttpClientFactory _clientFactory;
    private readonly ITokenManagementService _tokenManagementService;

    public HanaAuthenticateService(IHttpClientFactory clientFactory, ITokenManagementService tokenManagementService)
    {
        _tokenManagementService = tokenManagementService;
        _clientFactory = clientFactory;
    }

    public async Task Authenticate(AuthenticateHanaDTO data)
    {
        var client = _clientFactory.CreateClient("ServiceLayer");

        using (var response = await client.PostAsJsonAsync(ApiEndpoint, data))
        {
            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content.ReadFromJsonAsync<HanaSessionDTO>();
                _tokenManagementService.SetSessionToken(resultData.SessionId);
            }

            var errorData = await response.Content.ReadFromJsonAsync<ServiceLayerResponse.Fail>();
            throw new Exception(errorData.Error.Message.Value);
        }
    }
}