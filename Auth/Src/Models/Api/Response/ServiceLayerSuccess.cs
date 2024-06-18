using System.Text.Json.Serialization;

namespace Auth.Models.Response;

public class ServiceLayerSuccess<T>
{
    [JsonPropertyName("odata.metadata")]
    public string Odatametadata { get; set; }
    public T Value { get; set; }

    [JsonPropertyName("odata.nextLink")]
    public string OdatanextLink { get; set; }
}
