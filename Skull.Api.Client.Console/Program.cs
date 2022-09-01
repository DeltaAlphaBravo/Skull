using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using Microsoft.AspNetCore.SignalR.Client;

using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

//var baseUrl = "https://dan-wap-skull.azurewebsites.net"; // config.GetValue<string>("skullUrl");
var baseUrl = "https://localhost:7174";

var signalR = new HubConnectionBuilder().WithUrl($"{baseUrl}/gameHub").Build();
signalR.On<string>("ReceiveMessage", m => Console.WriteLine(m));
await signalR.StartAsync();


var client = new RestClient($"{baseUrl}");

var request = new RestRequest
{
    Method = Method.Post,
    Resource = "api/game",
};

request.AddBody(3);

var response = await client.PostAsync(request);

Console.WriteLine(response.Content);

request = new RestRequest
{
    Method = Method.Post,
    Resource = "api/game/Booyah/player",
};

request.AddBody("\"Janet\"");

response = await client.PostAsync(request);

Console.WriteLine(response.Content);

await host.RunAsync();