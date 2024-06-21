using Auth.Domain.Models.Api;
using Auth.Models.Response;

namespace Auth.Services.Interfaces;

public interface ISapService
{
    public Task<ApiResponse> GetEmployeeByEmail(string email);
}