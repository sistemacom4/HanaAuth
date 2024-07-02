using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application.Models;
using Application.Services.Interfaces;
using Application.Errors;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private const string ApiEndpoint = "/b1s/v1/EmployeesInfo";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ITokenManagementService _tokenManagementService;

    public EmployeeRepository(IHttpClientFactory clientFactory, ITokenManagementService tokenService)
    {
        _clientFactory = clientFactory;
        _httpClient = clientFactory.CreateClient("ServiceLayer");
        _httpClient.DefaultRequestHeaders.Add("B1SESSION", _tokenManagementService?.GetSessionToken());
        _tokenManagementService = tokenService;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IEnumerable<Employee>> GetEmployeeByEmail(string email)
    {
        string queryFilter = $"$filter=eMail eq\'{email}\'&$top=1";

        using (var response = await _httpClient.GetAsync($"{ApiEndpoint}?{queryFilter}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content
                    .ReadFromJsonAsync<ServiceLayerResponse.Success<IEnumerable<Employee>>>();
                return resultData.Value;
            }

            var errorData = await response.Content.ReadFromJsonAsync<ServiceLayerResponse.Fail>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw NotFoundError.Build(response.StatusCode, errorData?.Error.Message.Value);
                case HttpStatusCode.BadRequest:
                    throw BadRequestError.Build(response.StatusCode, errorData?.Error.Message.Value);
                case HttpStatusCode.Unauthorized:
                    const int noSessionTokenCode = 301;
                    if (errorData?.Error.Code == noSessionTokenCode)
                    {
                        throw InvalidHanaSessionError.Build(HttpStatusCode.Forbidden,
                            InvalidHanaSessionError.DefaultMessage);
                    }
                    throw UnauthorizedError.Build(HttpStatusCode.Unauthorized, errorData.Error.Message.Value);
                case HttpStatusCode.Forbidden:
                    throw ForbiddenError.Build(response.StatusCode, errorData.Error.Message.Value);
                default:
                    throw InternalServerError.Build(response.StatusCode, errorData.Error.Message.Value);
            }
        }
    }
}