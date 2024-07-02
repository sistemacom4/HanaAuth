using Application.Services.Interfaces;
using Application.Usecases.Interfaces;

namespace Application.Usecases;

public class CheckHanaSessionValidUsecase : ICheckHanaSessionValidUsecase
{
    private readonly ITokenManagementService _tokenManagementService;

    public CheckHanaSessionValidUsecase(ITokenManagementService tokenManagementService)
    {
        _tokenManagementService = tokenManagementService;
    }

    public bool Run()
    {
        return _tokenManagementService.IsSessionTokenValid();
    }
}