using Xunit;
using Amazon.Lambda.Core;
using System;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

namespace ReverbFunction.Tests;

public class FunctionTest
{
    public FunctionTest()
    {
    }

    [Fact]
    public async void TestDoWork() 
    {
        var functionParams = new ReverbFunctionParameters
        {
            GassyNewUri =  "http://localhost:5200/listings/new",
            GassyAuthUri = "http://localhost:5200/users/authenticate",
            ReverbUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24",
            MinutesSinceLastRun = 60,
            UserName = "ReverbAgent",
            UserPassword = "password"
        };

        var context = new TestLambdaContext();
        Function function = new Function();
        var result = await function.DoWork(functionParams, context);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async void TestGetNewEurorackListings() 
    {
        
        var reverbUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24";
        var lastRun = DateTime.Now.AddMinutes(-30);        
        var context = new TestLambdaContext();
        Function function = new Function();
        var listings = await function.GetNewEurorackListings(lastRun, reverbUri);
        foreach(var listing in listings) {
            Console.WriteLine(lastRun);
            Console.WriteLine(listing.make);
            Console.WriteLine(listing.model);
            Console.WriteLine($"Published At: {listing.published_at}");
            //Console.WriteLine(JsonSerializer.Serialize(listing)); 
            Console.WriteLine("...........................");
            Console.WriteLine("...........................");
            Console.WriteLine("...........................");
            Console.WriteLine("...........................");
        }

        Assert.NotEmpty(listings);
    }


}