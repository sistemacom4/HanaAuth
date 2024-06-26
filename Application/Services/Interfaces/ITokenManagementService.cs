namespace Application.Services.Interfaces;

public interface ITokenManagementService
{
    public string? GetSessionToken();
    public void SetSessionToken(string token);
}