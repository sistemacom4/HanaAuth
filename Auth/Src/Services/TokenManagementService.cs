namespace Auth.Services;

public class TokenManagementService
{
    private string? Token { get; set; }

    public string? GetToken()
    {
        return Token;
    }

    public void SetToken(string? token)
    {
        Token = token;
    }
}