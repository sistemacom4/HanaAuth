using System.Text.Json.Serialization;

namespace Application.Models;

public record Message(string Lang, string Value)
{
    [JsonPropertyName("lang")] public string Lang { get; } = Lang;

    [JsonPropertyName("value")] public string Value { get; } = Value;
};