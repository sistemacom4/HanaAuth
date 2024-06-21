using System.Text.Json;
using Auth.Domain.Models.Api;
using Auth.Domain.Models.Api.Response;
using Auth.Entities;
using Auth.Models.Response;
using Auth.Services.Interfaces;

namespace Auth.Services;

public class SapService : ISapService
{
    private const string ApiEndpoint = "/EmployeesInfo";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    public SapService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<ApiResponse> GetEmployeeByEmail(string email)
    {
        string queryFilter = $"$filter=eMail eq '{email}'&$top=1";

        var client = _clientFactory.CreateClient("ServiceLayer");

        using (var response = await client.GetAsync($"{ApiEndpoint}?{queryFilter}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content.ReadFromJsonAsync<ServiceLayerSuccess<List<Employee>>>();
                return ServiceLayerResponse<List<Employee>>.Success(resultData);
            }
            var errorData = await response.Content.ReadFromJsonAsync<ServiceLayerError>();
            return ServiceLayerResponse<ServiceLayerError>.Fail(errorData, errorData.Error.Message.Value);
        }
    }
}