using Application.DTOs;

namespace Application.Usecases.Interfaces;

public interface IAuthenticateUserUsecase
{
    public Boolean Run(LoginDTO data, string password);
}