using ReverbFunction.Models;
using System;
using ReverbFunction.ReverbModels;

namespace ReverbFunction
{
    public static class Helpers
    {
        public static ListingDto ConvertToListingDto(Listing listing){
            return new ListingDto {
                SiteId = listing.id,
                Make = listing.make,
                Model = listing.model,
                Price = Convert.ToDecimal(listing.price?.amount_cents ?? 0),
                Shipping = Convert.ToDecimal(0), //Convert.ToDecimal(listing.shipping?.us_rate?.amount_cents ?? 0),
                ItemDescription = listing.description,
                ItemCondition = "",//listing.condition_slug,
                Link = listing._links.self.href,
                OffersEnabled = listing.offers_enabled,
                CreatedAt = listing.created_at,
                UpdatedAt = listing.published_at
            };
        }
    }
}

