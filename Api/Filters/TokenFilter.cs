using Application.DTOs;
using Application.Services.Interfaces;
using Application.Usecases.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class TokenFilter : IAsyncActionFilter
{
    private readonly ITokenManagementService _tokenManagementService;
    private readonly IHanaAuthenticateUsecase _hanaAuthenticateUsecase;
    private readonly ICheckHanaSessionValidUsecase _sessionValidUsecase;
    private readonly IConfiguration _config;

    public TokenFilter(ITokenManagementService tokenManagementService, IHanaAuthenticateUsecase hanaAuthenticateUsecase, ICheckHanaSessionValidUsecase sessionValidUsecase, IConfiguration config)
    {
        _tokenManagementService = tokenManagementService;
        _hanaAuthenticateUsecase = hanaAuthenticateUsecase;
        _sessionValidUsecase = sessionValidUsecase;
        _config = config;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (string.IsNullOrEmpty(_tokenManagementService.GetSessionToken()) ||
            _sessionValidUsecase.Run())
        {

            await _hanaAuthenticateUsecase.Run();
        }

        await next();
    }
}