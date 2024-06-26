using System.Net;
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
        private readonly IHanaAuthenticateUsecase _hanaAuthenticateUsecase;
        private readonly ITokenManagementService _tokenManagementService;

        public LoginController(IGetEmployeeByEmailUsecase employeeByEmailUsecase,
            IHanaAuthenticateUsecase hanaAuthenticateUsecase,
            ITokenManagementService tokenManagementService
            )
        {
            _employeeByEmailUsecase = employeeByEmailUsecase;
            _hanaAuthenticateUsecase = hanaAuthenticateUsecase;
            _tokenManagementService = tokenManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginDTO data)
        {
            if (_tokenManagementService.GetSessionToken() == null)
            {
                await _hanaAuthenticateUsecase.Run();
            }

            try
            {
                var result = await _employeeByEmailUsecase.Run(data);
                return Ok(result);
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
    }
}