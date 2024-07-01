using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Api.Models;
using Application.DTOs;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Usecases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IGetEmployeeByEmailUsecase _employeeByEmailUsecase;
        private readonly IAuthenticateUserUsecase _authenticateUserUsecase;
        private readonly IHanaAuthenticateUsecase _hanaAuthenticateUsecase;
        private readonly ITokenManagementService _tokenManagementService;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public LoginController(IGetEmployeeByEmailUsecase employeeByEmailUsecase,
            IHanaAuthenticateUsecase hanaAuthenticateUsecase,
            IAuthenticateUserUsecase authenticateUserUsecase,
            ITokenManagementService tokenManagementService,
            IJwtService jwtService,
            IConfiguration configuration
        )
        {
            _employeeByEmailUsecase = employeeByEmailUsecase;
            _hanaAuthenticateUsecase = hanaAuthenticateUsecase;
            _tokenManagementService = tokenManagementService;
            _jwtService = jwtService;
            _configuration = configuration;
            _authenticateUserUsecase = authenticateUserUsecase;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SignIn(LoginDTO data)
        {
            if (_tokenManagementService.GetSessionToken() == null)
            {
                await _hanaAuthenticateUsecase.Run();
            }

            try
            {
                var employee = await _employeeByEmailUsecase.Run(data);
                var result = _authenticateUserUsecase.Run(data, employee.Pager);

                if (!result)
                {
                    throw UnauthorizedError.Build(HttpStatusCode.Unauthorized, "Credenciais incorretas!");
                }
                
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Email),
                    new Claim(ClaimTypes.Surname, employee.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = _jwtService.GenerateAccessToken(authClaims, _configuration);
                var refreshToken = _jwtService.GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidMinutes"],
                    out int refreshTokenValidMinutes);


                return Ok(new
                {
                    UserName = employee.FirstName,
                    Email = employee.Email,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            catch (UnauthorizedError ex)
            {
                if (ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    await _hanaAuthenticateUsecase.Run();
                }

                throw;
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDTO data)
        {
            if (data is null)
            {
                throw BadRequestError.Build(HttpStatusCode.BadRequest, "Invalid client request");
            }

            if (ModelState.IsValid)
            {
                string? accessToken = data.AccessToken
                                      ?? throw InternalServerError.Build(HttpStatusCode.InternalServerError,
                                          "Invalid access Token");
                
                string? refreshToken = data.RefreshToken
                                       ?? throw InternalServerError.Build(HttpStatusCode.InternalServerError,
                                           "Invalid refresh Token");

                var principal = _jwtService.GetPrincipalFromExpiredToken(data.AccessToken!, _configuration);

                if (principal is null)
                {
                    throw BadRequestError.Build(HttpStatusCode.BadRequest, "Invalid access/refresh token");
                }

                string email = principal.Identity.Name;
                
                var result = principal.
            }
        }
    }
}