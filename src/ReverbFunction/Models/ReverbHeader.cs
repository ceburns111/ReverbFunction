using ReverbFunction.Models;
using System.Text.Json.Serialization;

namespace ReverbFunction.Models
{
    public class ReverbResponse
    {
        [JsonPropertyName("total")]
        public long Total { get; set; }

        [JsonPropertyName("current_page")]
        public long CurrentPage { get; set; }

        [JsonPropertyName("total_pages")]
        public long TotalPages { get; set; }

        [JsonPropertyName("listings")]
        public ReverbListing[] Listings { get; set; }

        [JsonPropertyName("ships_to")]
        public string ShipsTo { get; set;}

        [JsonPropertyName("_links")]
        public ResponseLinks Links {get; set; }
    }
}