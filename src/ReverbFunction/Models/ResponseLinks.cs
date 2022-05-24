using System.Text.Json.Serialization;

namespace ReverbFunction.Models
{
    public class ResponseLinks
    {
        [JsonPropertyName("next")]
        public Next NextPageLink { get; set; }
    }
}