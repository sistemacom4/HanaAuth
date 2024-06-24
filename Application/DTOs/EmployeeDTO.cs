using System.Text.Json.Serialization;

namespace Application.DTOs;

public record EmployeeDTO
{
    [JsonPropertyName("EmployeeID")]
    public int EmployeeId { get; }
    
    [JsonPropertyName("FirstName")]
    public string FirstName { get; }
    
    [JsonPropertyName("LastName")]
    public string LastName { get; }
    
    [JsonPropertyName("eMail")]
    public string Email { get; }

    public EmployeeDTO(int employeeId, string firstName, string lastName, string email)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
};