// using ExchangeSharp;
// using Newtonsoft.Json;
//
// var publicKey = "r0PKnX6CfxaVlvCJ/JFOlYzRozG/rV0GkOK5lWBo/gL7XRNkvNzDOMtQ";
// var privateKey = "Ejp5noPZLpA71caRhWS/eaiCwkzO5BnQUboyMawhOBVpPccXZ4iuiFa2vm087umhtAYKuvoYv3uQI8Yr4Gk8Rg==";
//
// using var api = await ExchangeAPI.GetExchangeAPIAsync<ExchangeKrakenAPI>();
//
// api.PublicApiKey = publicKey.ToSecureString();
// api.PrivateApiKey = privateKey.ToSecureString();
//
// // var socket = await api.GetOrderDetailsListWebSocketAsync(orders =>
// // {
// // 	var json = JsonConvert.SerializeObject(orders, Formatting.Indented);
// //
// // 	Console.WriteLine("Received Orders");
// // 	Console.WriteLine(json);
// // });
// //
// // Console.WriteLine("Press any key to exit");
// //
// // Console.ReadKey();
//
//
// var order = await api.GetOrderDetailsAsync("OTNGSP-AM4SL-FGFFBT");
//
// var a = 5;

// place binance margin order

using ExchangeSharp;
using Newtonsoft.Json;

var binance = await ExchangeAPI.GetExchangeAPIAsync<ExchangeBinanceAPI>();

var publicKey = "YtGvdt6kHwxmISno2xu9rSfn1QqqT8AsalaAV0wQoTsBY6yn2Jciv4igcqH5KlNf";
var privateKey = "9xt33mBKQlVrCgylCoTaScxRlwMORbDYQz9guo0UDeAkUxy5pjR3RQzZqhjVD10q";

binance.LoadAPIKeysUnsecure(publicKey, privateKey);

using var conn = await binance.GetUserDataWebSocketAsync(data =>
{
	Console.WriteLine("aaaa");
	Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
	var a = 5;
}, margin: true);

Console.WriteLine("Press any key to exit");

Console.ReadKey();
//
// using Binance.Net.Clients;
// using CryptoExchange.Net.Authentication;
// using Newtonsoft.Json;
//
// var publicKey = "YtGvdt6kHwxmISno2xu9rSfn1QqqT8AsalaAV0wQoTsBY6yn2Jciv4igcqH5KlNf";
// var privateKey = "9xt33mBKQlVrCgylCoTaScxRlwMORbDYQz9guo0UDeAkUxy5pjR3RQzZqhjVD10q";
//
// var credentials = new ApiCredentials(publicKey, privateKey);
//
// var binanceSocketClient = new BinanceSocketClient(options => options.ApiCredentials = credentials);
//
// var listenKey = await binanceSocketClient.SpotApi.Account.StartUserStreamAsync();
//
// var tickerUpdatesSocket = await binanceSocketClient.SpotApi.Account.SubscribeToUserDataUpdatesAsync(listenKey.Data.Result,
// 	data => { Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented)); });
//
