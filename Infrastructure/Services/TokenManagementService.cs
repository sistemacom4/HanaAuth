using Application.DTOs;
using Application.Services.Interfaces;

namespace Infrastructure.Services;

public class TokenManagementService : ITokenManagementService
{
    private string? SessionToken { get; set; }
    private DateTime SessionExpiresAt { get; set; }
    
    public string? GetSessionToken()
    {
        return SessionToken;
    }

    public void SetSessionToken(HanaSessionDTO token)
    {
        SessionToken = token.SessionId;
        SessionExpiresAt = DateTime.Now.Add(TimeSpan.FromMinutes(token.SessionTimeout));
    }

    public bool IsSessionTokenValid()
    {
        return DateTime.Now > SessionExpiresAt ;
    }
}