using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class TokenFilter : IAsyncActionFilter
{
    private readonly ITokenManagementService _tokenManagementService;
    private readonly IHanaAuthenticateService _hanaAuthenticateService;
    private readonly IConfiguration _config;

    public TokenFilter(ITokenManagementService tokenManagementService, IHanaAuthenticateService hanaAuthenticateService, IConfiguration config)
    {
        _tokenManagementService = tokenManagementService;
        _hanaAuthenticateService = hanaAuthenticateService;
        _config = config;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (string.IsNullOrEmpty(_tokenManagementService.GetSessionToken()) || _tokenManagementService.IsSessionTokenValid())
        {
            var credentials = new AuthenticateHanaDTO{
                CompanyDB = _config["HanaCredentials:CompanyDB"],
                Password = _config["HanaCredentials:Password"],
                UserName = _config["HanaCredentials:UserName"]
            };
            await _hanaAuthenticateService.Authenticate(credentials);
        }

        await next();
    }
}