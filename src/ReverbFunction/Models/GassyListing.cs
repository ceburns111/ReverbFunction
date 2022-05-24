using System.Text.Json.Serialization;

namespace ReverbFunction.Models
{
    public class GassyListing
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("siteid")]
        public string SiteId { get; set; }

        [JsonPropertyName("make")]
        public string? Make { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime ListingCreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime ListingUpdatedAt { get; set; }

        [JsonPropertyName("description")]
        public string? ItemDescription { get; set; }

        [JsonPropertyName("condition")]
        public string? ItemCondition { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("shipping")]
        public decimal Shipping{ get; set; }

        [JsonPropertyName("offers_enabled")]
        public bool OffersEnabled { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }

        public DateTime UpdatedAt { get;set; }
    }
}