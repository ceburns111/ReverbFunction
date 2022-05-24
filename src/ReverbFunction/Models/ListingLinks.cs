using System.Text.Json.Serialization;
using ReverbFunction.Models;


namespace ReverbFunction.Models
{
    public class ListingLinks
    {
        [JsonPropertyName("photo")]
        public Photo photo { get; set; }

        [JsonPropertyName("self")]
        public Self self { get; set; }

        [JsonPropertyName("update")]
        public Update update { get; set; }

        [JsonPropertyName("end")]
        public End end { get; set; }

        [JsonPropertyName("want")]
        public Want want { get; set; }

        [JsonPropertyName("unwant")]
        public Unwant unwant { get; set; }

        [JsonPropertyName("edit")]
        public Edit edit { get; set; }

        [JsonPropertyName("web")]
        public Web web { get; set; }

        [JsonPropertyName("make_offer")]
        public MakeOffer make_offer { get; set; }

        [JsonPropertyName("add_to_wishlist")]
        public AddToWishlist add_to_wishlist { get; set; }

        [JsonPropertyName("remove_from_wishlist")]
        public RemoveFromWishlist remove_from_wishlist { get; set; }

        [JsonPropertyName("cart")]
        public Cart cart { get; set; }

        [JsonPropertyName("next")]
        public Next next { get; set; }

        [JsonPropertyName("suggestion")]
        public Suggestion suggestion { get; set; }
    }

    public class Suggestion
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Photo
    {
        [JsonPropertyName("")]
        public ListingLinks _links { get; set; }
    }

    public class Photo2
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Self
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Update
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class End
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Want
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Unwant
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Edit
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Web
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class MakeOffer
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class AddToWishlist
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class RemoveFromWishlist
    {
        [JsonPropertyName("method")]
        public string method { get; set; }

        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Cart
    {
        [JsonPropertyName("href")]
        public string href { get; set; }
    }
}