using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ReverbFunction.ReverbModels;
using ReverbFunction.Models;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ReverbFunction;

public class Function
{
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Function() 
    {
    }
    
    public async Task<string> DoWork(ReverbFunctionParameters functionParams, ILambdaContext context) {
        string reverbListingsUri = functionParams.ReverbUri;
        string authUri = functionParams.GassyAuthUri; 
        string newUri = functionParams.GassyNewUri; 
        string userName = functionParams.UserName;
        string userPassword = functionParams.UserPassword;
         
        var lastRunDate = DateTime.Now.AddMinutes(functionParams.MinutesSinceLastRun * -1); 
     
        var newEuroListings = await GetNewEurorackListings(lastRunDate, reverbListingsUri);
        var listingDtos = new List<ListingDto>();
        foreach (var euroListing in newEuroListings) {
            var dto = new ListingDto {
                SiteId = euroListing.id.ToString(),
                Make = euroListing.make,
                Model = euroListing.model,
                Price = Convert.ToDecimal(euroListing.price?.amount_cents ?? 0),
                Shipping = Convert.ToDecimal(0), //Convert.ToDecimal(listing.shipping?.us_rate.amount_cents ?? 0),
                ItemDescription = euroListing.description,
                ItemCondition = "", 
                Category = Category.eurorack,
                Link = euroListing._links.self.href,
                OffersEnabled = euroListing.offers_enabled,
                CreatedAt = euroListing.created_at,
                UpdatedAt = euroListing.published_at
            };
            listingDtos.Add(dto);
        }
 
        var user = await Authenticate(authUri, userName, userPassword);
        foreach(var listing in listingDtos) {
            await AddListingToGassy(listing, newUri, user.token);
        }

        return "1";
    }

    public Category GetProductCategory(string category) {
        if (category == "eurorack") 
            return Category.eurorack;
        if (category == "pedals") 
            return Category.pedals;
        if (category == "analog-synths" | category == "digital-synths") 
            return Category.synthesizers;
        return Category.eurorack;
    }

   
    public async Task<IEnumerable<Listing>> GetNewEurorackListings(DateTime lastRun, string listingsUri) {
        var Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/hal+json")
            );
       Client.DefaultRequestHeaders.Add("Accept-Version", "3.0");

        DateTime? maxPublishDate = null;
        List<Listing> newListings = new();

        //there has to be a better way to do this.... 
        do {Console.WriteLine($"Calling url: {listingsUri}");

            var streamTask = await Client.GetStreamAsync(listingsUri);
            var response = JsonSerializer.Deserialize<Root>(streamTask);

            if (newListings != null && response != null) {
                newListings.AddRange(response.listings);
            }

            maxPublishDate = newListings?.Min(x => x.published_at);
            listingsUri = response?._links.next.href ?? "";
        } while (!string.IsNullOrEmpty(listingsUri) && maxPublishDate > lastRun);  
        return newListings.Where(x => x.published_at > lastRun);
    }

    public async Task<UserResponse> Authenticate(string authUri, string userName, string userPassword) {
        var Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
            );
        
        var user = new User {
            UserName = userName,
            UserPassword = userPassword,
        };

        var userJson = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                System.Net.Mime.MediaTypeNames.Application.Json
            );

        HttpResponseMessage authMessage = await Client.PostAsync(authUri, userJson);
        string messageContentStr = await authMessage.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<UserResponse>(messageContentStr, new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true
        });

        //authMessage.EnsureSuccessStatusCode(); 
        return response;
    }

    public async Task<HttpResponseMessage> AddListingToGassy(ListingDto listing, string postUri, string token) {
        var Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
            );

        var listingJson = new StringContent(
            JsonSerializer.Serialize(listing),
            Encoding.UTF8,
            System.Net.Mime.MediaTypeNames.Application.Json
        );
        
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return await Client.PostAsync(postUri, listingJson); 
    
    }
}

