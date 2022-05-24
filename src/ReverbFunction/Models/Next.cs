using System.Text.Json.Serialization;

namespace ReverbFunction.Models
{
    public class Next
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }
}