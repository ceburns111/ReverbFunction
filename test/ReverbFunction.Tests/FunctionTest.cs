using Xunit;
using ReverbFunction.Models; 
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;


namespace ReverbFunction.Tests;

public class FunctionTest
{
    public FunctionTest()
    {
    }

    [Fact]
    public async void TestAddListings() 
    {
        var functionParams = new ReverbFunctionParameters
        {
            GassyNewUri =  "http://54.91.167.196/listings/new",
            GassyAuthUri = "http://54.91.167.196/agent/authenticate",
            ReverbUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24",
            MinutesSinceLastRun = 30
        };

        var context = new TestLambdaContext();
        Function function = new Function();
        var result = await function.AddListings(functionParams, context);
        Console.WriteLine(result);
        Assert.NotEmpty(result);
    }

    //  [Fact]
    // public async void TestGetAuthToken()
    // {
    //     var function = new Function(); 
    //     var token = await function.GetGassyAuthToken("http://54.80.11.168/agent/authenticate");
    //     Console.WriteLine($"TOKEN --> {token}");
    //     Assert.NotEqual(token, "");

    //     // DateTime lastRun = DateTime.Now.AddMinutes(-30);
    //     // // var reverbCategory = "keyboards-and-synths";
    //     // // var reverbProductType = "eurorack";
    //     // // var reverbListingsUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24";

    //     // TestLambdaContext context;
    //     // context = new TestLambdaContext();

    //     // Function function = new Function();
    //     // var listings = await function.AddListings(context);
    //     // foreach(var listing in listings) {
    //     //     Console.WriteLine(listing.Make);
    //     // }
        
    //     // Assert.Equal(true, true); 
    // }
}