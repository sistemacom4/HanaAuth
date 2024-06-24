using Application.Services.Interfaces;

namespace Application.Services;

public class TokenManagementService : ITokenManagementService
{
    private string? SessionToken { get; set; }
    
    public string? GetSessionToken()
    {
        return SessionToken;
    }

    public void SetSessionToken(string token)
    {
        SessionToken = token;
    }
}