using System.Net.Http.Json;
using System.Text.Json;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private const string ApiEndpoint = "/EmployeesInfo";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    public EmployeeRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IEnumerable<Employee>> GetEmployeeByEmail(string email)
    {
        string queryFilter = $"$filter=eMail eq '{email}'&$top=1";

        var client = _clientFactory.CreateClient("ServiceLayer");

        using (var response = await client.GetAsync($"{ApiEndpoint}?{queryFilter}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content
                    .ReadFromJsonAsync<ServiceLayerResponse.Success<IEnumerable<Employee>>>();
                return resultData.Value;
            }

            //
            var errorData = await response.Content.ReadFromJsonAsync<ServiceLayerResponse.Fail>();
            throw new Exception(message: errorData.Error.Message.Value);
        }
    }
}