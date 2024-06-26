using System.Text.Json.Serialization;

namespace Application.DTOs;

public record EmployeeDTO
{
    [JsonPropertyName("EmployeeID")]
    public int EmployeeId { get; init; }
    
    [JsonPropertyName("FirstName")]
    public string FirstName { get; init; }
    
    [JsonPropertyName("LastName")]
    public string LastName { get; init; }
    
    [JsonPropertyName("eMail")]
    public string Email { get; init; }
    
};