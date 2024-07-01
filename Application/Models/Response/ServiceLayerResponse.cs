using System.Text.Json.Serialization;

namespace Application.Models;

public sealed class ServiceLayerResponse
{
    public class Success<T>
    {
        [JsonPropertyName("odata.metadata")]
        public string Odatametadata { get; set; }
        
        [JsonPropertyName("Value")]
        public T Value { get; set; }

        [JsonPropertyName("odata.nextLink")]
        public string OdatanextLink { get; set; }
        
    }
    
    public class Fail
    {
        [JsonPropertyName("error")]
        public Error Error { get; private set; }

        public Fail(Error error)
        {
            Error = error;
        }
    }
}