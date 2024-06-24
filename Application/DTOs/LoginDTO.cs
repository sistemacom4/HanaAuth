using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record LoginDTO(string Email, string Password)
{
    [Required] [EmailAddress] public string Email { get; } = Email;

    [Required] public string Password { get; } = Password;

};