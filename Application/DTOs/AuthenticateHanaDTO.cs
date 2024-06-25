using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs;

public record AuthenticateHanaDTO
{
    [Required]
    [JsonPropertyName("CompanyDB")]
    public string CompanyDB { get; init; }

    [JsonPropertyName("Password")]
    [Required] public string Password { get; init; }

    [JsonPropertyName("UserName")]
    [Required] public string UserName { get; init; }
}