using Application.DTOs;

namespace Application.Services.Interfaces;

public interface IHanaAuthenticateService
{
    public Task<string> Authenticate(AuthenticateHanaDTO data);
}