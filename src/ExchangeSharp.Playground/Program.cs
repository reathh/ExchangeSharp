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

var binance = await ExchangeAPI.GetExchangeAPIAsync<ExchangeBinanceAPI>();

var publicKey = "YtGvdt6kHwxmISno2xu9rSfn1QqqT8AsalaAV0wQoTsBY6yn2Jciv4igcqH5KlNf";
var privateKey = "9xt33mBKQlVrCgylCoTaScxRlwMORbDYQz9guo0UDeAkUxy5pjR3RQzZqhjVD10q";

binance.LoadAPIKeysUnsecure(publicKey, privateKey);

// get price for BTC FDUSD
var price = await binance.GetTickerAsync("BTCFDUSD");

// var order = await binance.PlaceOrderAsync(new ExchangeOrderRequest
// {
// 		Amount = 0.0001m,
// 		IsBuy = true,
// 		Price = 0.1m,
// 		MarketSymbol = "BTC",
// 		OrderType = ExchangeSharp.OrderType.Limit
// });
//
var a = 5;
