using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application.DTOs;
using Application.Errors;
using Application.Models;
using Application.Services;
using FluentAssertions;
using InfrastructureTests.Tools;
using Moq;
using Xunit;

namespace InfrastructureTests;

public class HanaAuthenticateServiceTests
{
    private readonly Mock<IHttpClientFactory> _clientFactoryMock;

    // private readonly HanaAuthenticateService _hanaAuthenticateService;
    private const string BaseUri = "https://com4-suse-sap-hana:50000";

    public HanaAuthenticateServiceTests()
    {
        _clientFactoryMock = new Mock<IHttpClientFactory>();
    }

    private HttpClient CreateMockHttpClient<T>(HttpStatusCode statusCode, T response)
    {
        var mockHandler = new MockHttpMessageHandler();
        mockHandler.When(HttpMethod.Post, "/b1s/v1/Login")
            .Respond(statusCode, JsonContent.Create(response));

        var client = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(BaseUri)
        };

        return client;
    }

    [Fact]
    public async Task Authenticate_ReturnsSessionId_OnSuccess()
    {
        // Arrange
        var authenticateHanaDTO = new AuthenticateHanaDTO();
        var expectedSessionId = "123456";
        var hanaSessionDTO = new HanaSessionDTO { SessionId = expectedSessionId };

        var client = CreateMockHttpClient(HttpStatusCode.OK, hanaSessionDTO);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var hanaAuthenticateService = new HanaAuthenticateService(_clientFactoryMock.Object);

        // Act
        var result = await hanaAuthenticateService.Authenticate(authenticateHanaDTO);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expectedSessionId);
    }

    [Fact]
    public async Task Authenticate_ReturnsSessionId_OnNotFound()
    {
        // Arrange
        var authenticateHanaDTO = new AuthenticateHanaDTO();
        var expected =
            new ServiceLayerResponse.Fail(new Error(Code: 404,
                Message: new Message(Lang: "PT", Value: "Nada encontrado!")));
        var client = CreateMockHttpClient(HttpStatusCode.NotFound, expected);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var hanaAuthenticateService = new HanaAuthenticateService(_clientFactoryMock.Object);

        // Assert
        await Assert.ThrowsAsync<NotFoundError>(async () =>
            await hanaAuthenticateService.Authenticate(authenticateHanaDTO));
    }
    
    
    [Fact]
    public async Task Authenticate_ReturnsSessionId_OnBadRequest()
    {
        // Arrange
        var authenticateHanaDTO = new AuthenticateHanaDTO();
        var expected =
            new ServiceLayerResponse.Fail(new Error(Code: 400,
                Message: new Message(Lang: "PT", Value: "Bad request")));
        var client = CreateMockHttpClient(HttpStatusCode.BadRequest, expected);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var hanaAuthenticateService = new HanaAuthenticateService(_clientFactoryMock.Object);

        // Assert
        await Assert.ThrowsAsync<BadRequestError>(async () =>
            await hanaAuthenticateService.Authenticate(authenticateHanaDTO));
    }
    
    [Fact]
    public async Task Authenticate_ReturnsSessionId_OnInternalServerError()
    {
        // Arrange
        var authenticateHanaDTO = new AuthenticateHanaDTO();
        var expected =
            new ServiceLayerResponse.Fail(new Error(Code: 500,
                Message: new Message(Lang: "PT", Value: "Unexpected Error")));
        var client = CreateMockHttpClient(HttpStatusCode.InternalServerError, expected);
        _clientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        var hanaAuthenticateService = new HanaAuthenticateService(_clientFactoryMock.Object);

        // Assert
        await Assert.ThrowsAsync<InternalServerError>(async () =>
            await hanaAuthenticateService.Authenticate(authenticateHanaDTO));
    }
}