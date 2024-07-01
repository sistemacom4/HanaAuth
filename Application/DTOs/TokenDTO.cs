using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public record TokenDTO
{
    [Required]
    public string? AccessToken { get; init; }
    
    [Required]
    public string? RefreshToken { get; init; }
}