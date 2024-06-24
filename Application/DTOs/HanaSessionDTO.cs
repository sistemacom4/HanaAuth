using System.Text.Json.Serialization;

namespace Application.DTOs;

public record HanaSessionDTO()
{
    [JsonPropertyName("odata.metadata")]
    public string? OdataMetada { get; set; }
    
    [JsonPropertyName("SessionId")]
    public string SessionId { get; set; }
    
    [JsonPropertyName("Version")]
    public string? Version { get; set; }
    
    [JsonPropertyName("SessionTimeout")]
    public int SessionTimeout { get; set; }
}