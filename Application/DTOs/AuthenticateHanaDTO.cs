using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record AuthenticateHanaDTO(string CompanyDb, string Password, string UserName)
{
    [Required]
    public string CompanyDb { get; } = CompanyDb;

    [Required]
    public string Password { get; } = Password;

    [Required]
    public string UserName { get; } = UserName;
};