using System.Text.Json.Serialization;

namespace Application.DTOs;

public record HanaSessionDTO()
{
    [JsonPropertyName("odata.metadata")]
    public string? OdataMetada { get; init; }
    
    [JsonPropertyName("SessionId")]
    public string SessionId { get; init; }
    
    [JsonPropertyName("Version")]
    public string? Version { get; init; }
    
    [JsonPropertyName("SessionTimeout")]
    public int SessionTimeout { get; init; }
}