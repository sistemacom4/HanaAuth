using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application.Errors;
using Application.Models;
using Application.Services;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using InfrastructureTests.Tools;
using Moq;

namespace InfrastructureTests;

public class EmployeeRepositoryTests
{
    private readonly Mock<IHttpClientFactory> _clientFactoryMock;
    private readonly TokenManagementService _tokenManagementService;
    private const string BaseUri = "https://com4-suse-sap-hana:50000";
    private const string PathUri = "/b1s/v1/EmployeesInfo";

    public EmployeeRepositoryTests()
    {
        _clientFactoryMock = new Mock<IHttpClientFactory>();
        _tokenManagementService = new TokenManagementService();
    }

    private HttpClient CreateMockHttpClient<T>(HttpStatusCode statusCode, T response)
    {
        var mockHandler = new MockHttpMessageHandler();
        mockHandler.When(HttpMethod.Get, PathUri)
            .Respond(statusCode, JsonContent.Create(response));

        var client = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(BaseUri)
        };

        return client;
    }

    [Fact]
    public async Task GetEmployeeByEmail_ReturnsSuccess_WhenEmployeeExists()
    {
        //Arrange
        var email = "exemple.exemple@com4.com.br";
        var faker = new Faker<Employee>()
            .RuleFor(e => e.eMail, email);
        var expectedEmployee = faker.Generate();
        var response = new ServiceLayerResponse.Success<List<Employee>>
        {
            Value = [expectedEmployee]
        };
        var client = CreateMockHttpClient(HttpStatusCode.OK, response);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var employeeRepository = new EmployeeRepository(_clientFactoryMock.Object, _tokenManagementService);

        var result = await employeeRepository.GetEmployeeByEmail(email);
        
        //Assert
        result.Should().NotBeNull();
        result.First().eMail.Should().BeEquivalentTo(email);
    }

    [Fact]
    public async Task GetEmployeeByEmail_ThrowsNotFoundError()
    {
        //Arrange
        var email = "exemple.exemple@com4.com.br";
        var expected =
            new ServiceLayerResponse.Fail(new Error(Code: 404,
                Message: new Message(Lang: "PT", Value: "Nada encontrado!")));
        
        var client = CreateMockHttpClient(HttpStatusCode.NotFound, expected);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var employeeRepository = new EmployeeRepository(_clientFactoryMock.Object, _tokenManagementService);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundError>(async () =>
            await employeeRepository.GetEmployeeByEmail(email));
    }
    
    [Fact]
    public async Task GetEmployeeByEmail_ThrowsBadRequest()
    {
        //Arrange
        var email = "exemple.exemple@com4.com.br";
        var expected =
            new ServiceLayerResponse.Fail(new Error(Code: 400,
                Message: new Message(Lang: "PT", Value: "Requsição mal formada!")));
        
        var client = CreateMockHttpClient(HttpStatusCode.BadRequest, expected);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var employeeRepository = new EmployeeRepository(_clientFactoryMock.Object, _tokenManagementService);
        
        //Assert
        await Assert.ThrowsAsync<BadRequestError>(async () =>
            await employeeRepository.GetEmployeeByEmail(email));
    }
    
    [Fact]
    public async Task GetEmployeeByEmail_ThrowsForbidden()
    {
        //Arrange
        var email = "exemple.exemple@com4.com.br";
        var expected =
            new ServiceLayerResponse.Fail(new Error(Code: 301,
                Message: new Message(Lang: "PT", Value: "Requsição mal formada!")));
        
        var client = CreateMockHttpClient(HttpStatusCode.Forbidden, expected);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var employeeRepository = new EmployeeRepository(_clientFactoryMock.Object, _tokenManagementService);
        
        //Assert
        await Assert.ThrowsAsync<ForbiddenError>(async () =>
            await employeeRepository.GetEmployeeByEmail(email));
    }
}