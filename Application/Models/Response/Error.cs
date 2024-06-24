using System.Text.Json.Serialization;

namespace Application.Models;

public record Error(int Code, Message Message)
{
    [JsonPropertyName("code")] public int Code { get; } = Code;
    [JsonPropertyName("message")] public Message Message { get; } = Message;
};