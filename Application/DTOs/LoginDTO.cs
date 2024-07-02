using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record LoginDTO
{
    [Required] [EmailAddress] public string Email { get; init; }
    [Required] public string Password { get; init; }

};