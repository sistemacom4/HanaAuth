using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Api.Filters;
using Application.DTOs;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Usecases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api")]
    [ApiController]
    [ServiceFilter(typeof(TokenFilter))]
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

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginDTO data)
        {
            try
            {
                var employee = await _employeeByEmailUsecase.Run(data.Email);
                var result = _authenticateUserUsecase.Run(data, employee.Pager);

                if (!result)
                {
                    throw InvalidCredentialsError.Build(HttpStatusCode.Unauthorized,
                        InvalidCredentialsError.DefaultMessage);
                }
                
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.FirstName),
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = _jwtService.GenerateAccessToken(authClaims, _configuration);
                
                return Ok(new
                {
                    UserName = employee.FirstName,
                    Email = employee.Email,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    HanaSession = _tokenManagementService.GetSessionToken(),
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

        [Authorize]
        [HttpGet("check_token")]
        public IActionResult Test()
        {
            return Ok("Token success!");
        }
    }
}