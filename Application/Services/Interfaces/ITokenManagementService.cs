using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ITokenManagementService
{
    public string? GetSessionToken();
    public void SetSessionToken(HanaSessionDTO session);

    public bool IsSessionTokenValid();
}