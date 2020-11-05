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
			//[
			// {
			// "symbol": "XBTUSD",
			// "rootSymbol": "XBT",
			// "state": "Open",
			// "typ": "FFWCSX",
			// "listing": "2016-05-13T12:00:00Z",
			// "front": "2016-05-13T12:00:00Z",
			// "expiry": null,
			// "settle": null,
			// "relistInterval": null,
			// "inverseLeg": "",
			// "sellLeg": "",
			// "buyLeg": "",
			// "optionStrikePcnt": null,
			// "optionStrikeRound": null,
			// "optionStrikePrice": null,
			// "optionMultiplier": null,
			// "positionCurrency": "USD",
			// "underlying": "XBT",
			// "quoteCurrency": "USD",
			// "underlyingSymbol": "XBT=",
			// "reference": "BMEX",
			// "referenceSymbol": ".BXBT",
			// "calcInterval": null,
			// "publishInterval": null,
			// "publishTime": null,
			// "maxOrderQty": 10000000,
			// "maxPrice": 1000000,
			// "lotSize": 1,
			// "tickSize": 0.5,
			// "multiplier": -100000000,
			// "settlCurrency": "XBt",
			// "underlyingToPositionMultiplier": null,
			// "underlyingToSettleMultiplier": -100000000,
			// "quoteToSettleMultiplier": null,
			// "isQuanto": false,
			// "isInverse": true,
			// "initMargin": 0.01,
			// "maintMargin": 0.0035,
			// "riskLimit": 20000000000,
			// "riskStep": 10000000000,
			// "limit": null,
			// "capped": false,
			// "taxed": true,
			// "deleverage": true,
			// "makerFee": -0.00025,
			// "takerFee": 0.00075,
			// "settlementFee": 0,
			// "insuranceFee": 0,
			// "fundingBaseSymbol": ".XBTBON8H",
			// "fundingQuoteSymbol": ".USDBON8H",
			// "fundingPremiumSymbol": ".XBTUSDPI8H",
			// "fundingTimestamp": "2020-11-05T12:00:00Z",
			// "fundingInterval": "2000-01-01T08:00:00Z",
			// "fundingRate": 0.0001,
			// "indicativeFundingRate": 0.0001,
			// "rebalanceTimestamp": null,
			// "rebalanceInterval": null,
			// "openingTimestamp": "2020-11-05T08:00:00Z",
			// "closingTimestamp": "2020-11-05T09:00:00Z",
			// "sessionInterval": "2000-01-01T01:00:00Z",
			// "prevClosePrice": 13732.02,
			// "limitDownPrice": null,
			// "limitUpPrice": null,
			// "bankruptLimitDownPrice": null,
			// "bankruptLimitUpPrice": null,
			// "prevTotalVolume": 2529364694703,
			// "totalVolume": 2529481515964,
			// "volume": 116821261,
			// "volume24h": 1921569556,
			// "prevTotalTurnover": 33921307095844156,
			// "totalTurnover": 33922114571231504,
			// "turnover": 807475387346,
			// "turnover24h": 13616044144378,
			// "homeNotional24h": 136160.44144378,
			// "foreignNotional24h": 1921569556,
			// "prevPrice24h": 13658.5,
			// "vwap": 14114.326,
			// "highPrice": 14570,
			// "lowPrice": 13610.5,
			// "lastPrice": 14449.5,
			// "lastPriceProtected": 14449.5,
			// "lastTickDirection": "ZeroPlusTick",
			// "lastChangePcnt": 0.0579,
			// "bidPrice": 14449,
			// "midPrice": 14449.25,
			// "askPrice": 14449.5,
			// "impactBidPrice": 14448.7791,
			// "impactMidPrice": 14449.25,
			// "impactAskPrice": 14449.5,
			// "hasLiquidity": true,
			// "openInterest": 401477864,
			// "openValue": 2778628296744,
			// "fairMethod": "FundingRate",
			// "fairBasisRate": 0.1095,
			// "fairBasis": 0.55,
			// "fairPrice": 14449.26,
			// "markMethod": "FairPrice",
			// "markPrice": 14449.26,
			// "indicativeTaxRate": 0,
			// "indicativeSettlePrice": 14448.71,
			// "optionUnderlyingPrice": null,
			// "settledPrice": null,
			// "timestamp": "2020-11-05T08:56:06.902Z"
			// }
			// ]

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
