using System.Net;
using Application.DTOs;
using Application.Errors;
using Application.Usecases.Interfaces;

namespace Application.Usecases;

public class AuthenticateUserUsecase : IAuthenticateUserUsecase
{
    public bool Run(LoginDTO data, string password)
    {
        if (data.Password != password)
        {
            throw UnauthorizedError.Build(HttpStatusCode.Unauthorized, "Credenciais incorretas!");
        }

        return true;
    }
}