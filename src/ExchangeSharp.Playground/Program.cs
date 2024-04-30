using System.Collections;
using ExchangeSharp;
using Newtonsoft.Json;

var publicKey = "r0PKnX6CfxaVlvCJ/JFOlYzRozG/rV0GkOK5lWBo/gL7XRNkvNzDOMtQ";
var privateKey = "Ejp5noPZLpA71caRhWS/eaiCwkzO5BnQUboyMawhOBVpPccXZ4iuiFa2vm087umhtAYKuvoYv3uQI8Yr4Gk8Rg==";

using var api = await ExchangeAPI.GetExchangeAPIAsync<ExchangeKrakenAPI>();

api.PublicApiKey = publicKey.ToSecureString();
api.PrivateApiKey = privateKey.ToSecureString();

var socket = await api.GetOrderDetailsListWebSocketAsync(orders =>
{
	var json = JsonConvert.SerializeObject(orders, Formatting.Indented);

	Console.WriteLine(json);
});

Console.WriteLine("Press any key to exit");

Console.ReadKey();
