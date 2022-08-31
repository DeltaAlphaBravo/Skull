using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System.Text.Json;

using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var baseUrl = "https://localhost:7174"; // config.GetValue<string>("skullUrl");

var client = new RestClient($"{baseUrl}");

var request = new RestRequest
{
    Method = Method.Post,
    Resource = "https://localhost:7174/api/game",
};

request.AddBody(3).AddHeader("accept", "text/plain");

var response = await client.GetAsync(request);

Console.WriteLine(JsonSerializer.Serialize(response));

await host.RunAsync();