using Application.DTOs;
using Application.Services.Interfaces;
using Application.Usecases.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Usecases;

public class HanaAuthenticateUsecase : IHanaAuthenticateUsecase
{
    private readonly IHanaAuthenticateService _authenticateService;
    private readonly ITokenManagementService _tokenManagementService;
    private readonly IConfiguration _config;

    public HanaAuthenticateUsecase(IHanaAuthenticateService authenticateService,
        ITokenManagementService tokenManagementService, IConfiguration config)
    {
        _authenticateService = authenticateService;
        _tokenManagementService = tokenManagementService;
        _config = config;
    }

    public async Task Run()
    {
        var credentials = new AuthenticateHanaDTO{
            CompanyDB = _config["HanaCredentials:CompanyDB"],
            Password = _config["HanaCredentials:Password"],
            UserName = _config["HanaCredentials:UserName"]
        };
        
        var token = await _authenticateService.Authenticate(credentials);
        _tokenManagementService.SetSessionToken(token);
    }
}