using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ExchangeSharp
{
	public sealed partial class ExchangeBitMEXAPI
	{
		protected override async Task<ExchangeTicker> OnGetTickerAsync(string marketSymbol)
		{
			Dictionary<string, object> payload = await GetNoncePayloadAsync();
			string query = $"/instrument?symbol={marketSymbol}";
			JToken response = await MakeJsonRequestAsync<JToken>(query, BaseUrl, payload, "GET");
			var instrument = response[0];
			return new ExchangeTicker()
			{
				Ask = instrument.Value<decimal>("askPrice"),
				Bid = instrument.Value<decimal>("bidPrice"),
				Last = instrument.Value<decimal>("lastPrice")
			};
		}
	}
}
