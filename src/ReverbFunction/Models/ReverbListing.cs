using System.Text.Json.Serialization;

namespace ReverbFunction.Models
{
    public class ReverbListing
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("make")]
        public string make { get; set; }

        [JsonPropertyName("model")]
        public string model { get; set; }

        [JsonPropertyName("finish")]
        public string finish { get; set; }

        [JsonPropertyName("year")]
        public string year { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime created_at { get; set; }

        [JsonPropertyName("shop_name")]
        public string shop_name { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        //[JsonPropertyName("condition")]
        //public Guid condition { get; set; }

        [JsonPropertyName("condition_uuid")]
        public string condition_uuid { get; set; }

        [JsonPropertyName("condition_slug")]
        public string condition_slug { get; set; }

        // [JsonPropertyName("price")]
        // public Price price { get; set; }

        [JsonPropertyName("inventory")]
        public int inventory { get; set; }

        [JsonPropertyName("has_inventory")]
        public bool has_inventory { get; set; }

        [JsonPropertyName("offers_enabled")]
        public bool offers_enabled { get; set; }

        [JsonPropertyName("auction")]
        public bool auction { get; set; }

        [JsonPropertyName("category_uuids")]
        public string[] category_uuids { get; set; }

        [JsonPropertyName("listing_currency")]
        public string listing_currency { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime published_at { get; set; }


        // [JsonPropertyName("buyer_price")]
        // public Buyer_Price buyer_price { get; set; }

        // [JsonPropertyName("state")]
        // public State state { get; set; }

        // [JsonPropertyName("shipping")]
        // public Shipping shipping { get; set; }

        [JsonPropertyName("slug")]
        public string slug { get; set; }

        [JsonPropertyName("_links")]
        public ListingLinks _links { get; set; }
    }
}