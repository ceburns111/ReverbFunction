using ReverbFunction.Models;
using ReverbFunction.ReverbModels;

namespace ReverbFunction
{
    public static class Helpers
    {
        public static ListingDto ConvertToListingDto(Listing listing){
            return new ListingDto {
                ReverbId = listing.id,
                Make = listing.make,
                Model = listing.model,
                Price = listing.price.amount_cents,
                //Shipping = listing.shipping?.us_rate?.amount_cents ?? 0,
                ItemDescription = listing.description ?? "",
                ItemCondition = listing.condition_slug ?? "",
                Link = listing._links.self.href,
                
            };
        }
    }
}