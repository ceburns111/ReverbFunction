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
        var reverbListingsUri = functionParams.ReverbUri;
        var authUri = functionParams.GassyAuthUri; 
        var newUri = functionParams.GassyNewUri; 
        var lastRunDate = DateTime.Now.AddMinutes(functionParams.MinutesSinceLastRun * -1); 
     
        var newReverbListings = await GetNewReverbListings(lastRunDate, reverbListingsUri);
        var listingDtos = new List<ListingDto>();
        foreach (var listing in newReverbListings) {
            listingDtos.Add(Helpers.ConvertToListingDto(listing));
        }
 
        var authToken = await GetGassyAuthToken(authUri);
        foreach(var listing in listingDtos) {
            await AddListingToGassy(listing, newUri, authToken);
        }

        return "1";
    }

   
    public async Task<IEnumerable<Listing>> GetNewReverbListings(DateTime lastRun, string listingsUri) {
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

    public async Task<string> GetGassyAuthToken(string authUri) {
        var Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
            );
        
        var agent = new Agent {
            AgentName = "ReverbAgent",
            AgentPassword = "password"
        };

        var agentJson = new StringContent(
                JsonSerializer.Serialize(agent),
                Encoding.UTF8,
                System.Net.Mime.MediaTypeNames.Application.Json
            );

        HttpResponseMessage authMessage = await Client.PostAsync(authUri, agentJson);
        string messageContentStr = await authMessage.Content.ReadAsStringAsync();
        var agentResponse = JsonSerializer.Deserialize<AgentResponse>(messageContentStr, new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true
        });

        //authMessage.EnsureSuccessStatusCode(); 
        return agentResponse.token;           
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

