using Xunit;
using ReverbFunction.Models; 
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
    public async void TestAddListings() 
    {
        var functionParams = new ReverbFunctionParameters
        {
            GassyNewUri =  "http://54.91.167.196/listings/new",
            GassyAuthUri = "http://54.91.167.196/agent/authenticate",
            ReverbUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24",
            MinutesSinceLastRun = 60
        };

        var context = new TestLambdaContext();
        Function function = new Function();
        var result = await function.AddListings(functionParams, context);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async void TestNewListings() 
    {
        
        var reverbUri = "https://reverb.com/api/listings?category=eurorack&product_type=keyboards-and-synths&page=1&per_page=24";
        var lastRun = DateTime.Now.AddMinutes(-30);        
        var context = new TestLambdaContext();
        Function function = new Function();
        var listings = await function.GetNewReverbListings(lastRun, reverbUri);
        foreach(var listing in listings) {
            Console.WriteLine(JsonSerializer.Serialize(listing)); 
            Console.WriteLine("...........................");
            Console.WriteLine("...........................");
            Console.WriteLine("...........................");
            Console.WriteLine("...........................");
        }

        Assert.NotEmpty(listings);
    }


}