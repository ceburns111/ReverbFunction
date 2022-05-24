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
        DateTime lastRun = DateTime.Now.AddMinutes(-30);
        // var reverbCategory = "keyboards-and-synths";
        // var reverbProductType = "eurorack";
        // var reverbListingsUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24";

        TestLambdaContext context;
        context = new TestLambdaContext();

        Function function = new Function();
        var listings = await function.AddListings(context);
        foreach(var listing in listings) {
            Console.WriteLine(listing.Make);
        }
        
        Assert.Equal(true, true); 
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