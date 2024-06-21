using System.Net;
using System.Net.Http.Json;
using Auth.Domain.Models.Api.Response;
using Auth.Entities;
using Auth.Models.Response;
using Auth.Services;
using Auth.Services.Interfaces;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

namespace AuthTests.Tests;

public class SapServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly SapService _sapService;
    private const string _baseUrl = "https://com4-suse-sap-hana:50000/b1s/v1";

    public SapServiceTests()
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        _sapService = new SapService(_mockHttpClientFactory.Object);
    }

    [Fact]
    public async Task GetEmployeeByEmail_ReturnsSuccess_WhenEmployeeExists()
    {
        // Arrange
        var email = "rafael.veronez@com4.com.br";
        var faker = new Faker<Employee>()
            .RuleFor(e => e.eMail, email);
        var expectedEmployee = faker.Generate();
        var response = new ServiceLayerSuccess<List<Employee>>
        {
            Value = [expectedEmployee]
        };
        var httpClient = CreateMockHttpClient(HttpStatusCode.OK, response);
        httpClient.BaseAddress = new Uri(_baseUrl);
        _mockHttpClientFactory.Setup(x => x.CreateClient("ServiceLayer"))
            .Returns(httpClient);
        
        //Act
        var result = await _sapService.GetEmployeeByEmail(email) 
            as ServiceLayerResponse<ServiceLayerSuccess<List<Employee>>>;
        
        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Data.Value.Should().NotBeEmpty();
        result.Data.Value.First().Should().BeEquivalentTo(expectedEmployee);
    }
    
    [Fact]
    public async Task GetEmployeeByEmail_ReturnsSuccess_WhenEmployeeNotExists()
    {
        // Arrange
        var email = "rafael.veronez@com4.com.br";
        var faker = new Faker<Employee>()
            .RuleFor(e => e.eMail, email);
        var expectedEmployee = faker.Generate();
        var response = new ServiceLayerSuccess<List<Employee>>
        {
            Value = []
        };
        var httpClient = CreateMockHttpClient(HttpStatusCode.OK, response);
        httpClient.BaseAddress = new Uri(_baseUrl);
        _mockHttpClientFactory.Setup(x => x.CreateClient("ServiceLayer"))
            .Returns(httpClient);
        
        //Act
        var result = await _sapService.GetEmployeeByEmail(email) 
            as ServiceLayerResponse<ServiceLayerSuccess<List<Employee>>>;
        
        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Data.Value.Should().BeEmpty();
    }

    private HttpClient CreateMockHttpClient<T>(HttpStatusCode statusCode, T response)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = JsonContent.Create(response)
            });

        return new HttpClient(mockHandler.Object);
    }
}