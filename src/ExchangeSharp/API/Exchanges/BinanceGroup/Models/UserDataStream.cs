using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExchangeSharp.BinanceGroup
{
	internal class ExecutionReport
	{
		[JsonPropertyName("e")]
		public string EventType { get; set; }

		[JsonPropertyName("E")]
		public long EventTime { get; set; }

		[JsonPropertyName("s")]
		public string Symbol { get; set; }

		[JsonPropertyName("c")]
		public string ClientOrderId { get; set; }

		[JsonPropertyName("S")]
		public string Side { get; set; }

		[JsonPropertyName("o")]
		public string OrderType { get; set; }

		[JsonPropertyName("f")]
		public string TimeInForce { get; set; }

		[JsonPropertyName("q")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal OrderQuantity { get; set; }

		[JsonPropertyName("p")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal OrderPrice { get; set; }

		[JsonPropertyName("P")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal StopPrice { get; set; }

		[JsonPropertyName("F")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal IcebergQuantity { get; set; }

		[JsonPropertyName("g")]
		public int OrderListId { get; set; }

		[JsonPropertyName("C")]
		public string OriginalClientOrderId { get; set; }

		[JsonPropertyName("x")]
		public string CurrentExecutionType { get; set; }

		[JsonPropertyName("X")]
		public string CurrentOrderStatus { get; set; }

		[JsonPropertyName("r")]
		public string OrderRejectReason { get; set; }

		[JsonPropertyName("i")]
		public long OrderId { get; set; }

		[JsonPropertyName("l")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal LastExecutedQuantity { get; set; }

		[JsonPropertyName("z")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal CumulativeFilledQuantity { get; set; }

		[JsonPropertyName("L")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal LastExecutedPrice { get; set; }

		[JsonPropertyName("n")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal CommissionAmount { get; set; }

		[JsonPropertyName("N")]
		public string CommissionAsset { get; set; }

		[JsonPropertyName("T")]
		public long TransactionTime { get; set; }

		[JsonPropertyName("t")]
		public long TradeId { get; set; }

		[JsonPropertyName("w")]
		public bool IsTheOrderWorking { get; set; }

		[JsonPropertyName("m")]
		public bool IsThisTradeTheMakerSide { get; set; }

		[JsonPropertyName("O")]
		public long OrderCreationTime { get; set; }

		[JsonPropertyName("Z")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal CumulativeQuoteAssetTransactedQuantity { get; set; }

		[JsonPropertyName("Y")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal LastQuoteAssetTransactedQuantity { get; set; }

		public override string ToString()
		{
			return
				$"{nameof(Symbol)}: {Symbol}, {nameof(OrderType)}: {OrderType}, {nameof(OrderQuantity)}: {OrderQuantity}, {nameof(OrderPrice)}: {OrderPrice}, {nameof(CurrentOrderStatus)}: {CurrentOrderStatus}, {nameof(OrderId)}: {OrderId}";
		}

		/// <summary>
		/// convert current instance to ExchangeOrderResult
		/// </summary>
		public ExchangeOrderResult ExchangeOrderResult
		{
			get
			{
				var status = BinanceGroupCommon.ParseExchangeAPIOrderResult(status: CurrentOrderStatus, amountFilled: CumulativeFilledQuantity);

				return new ExchangeOrderResult()
				{
					OrderId = OrderId.ToString(),
					ClientOrderId = ClientOrderId,
					Result = status,
					ResultCode = CurrentOrderStatus,
					Message = OrderRejectReason, // can use for multiple things in the future if needed
					AmountFilled = TradeId > 0 ? LastExecutedQuantity : CumulativeFilledQuantity,
					Price = OrderPrice,
					AveragePrice = CumulativeQuoteAssetTransactedQuantity / CumulativeFilledQuantity, // Average price can be found by doing Z divided by z.
					OrderDate = CryptoUtility.UnixTimeStampToDateTimeMilliseconds(OrderCreationTime),
					CompletedDate = status.IsCompleted() ? (DateTime?)CryptoUtility.UnixTimeStampToDateTimeMilliseconds(TransactionTime) : null,
					TradeDate = CryptoUtility.UnixTimeStampToDateTimeMilliseconds(TransactionTime),
					UpdateSequence = EventTime, // in Binance, the sequence number is also the EventTime
					MarketSymbol = Symbol,

					// IsBuy is not provided here
					Fees = CommissionAmount,
					FeesCurrency = CommissionAsset,
					TradeId = TradeId.ToString(),
				};
			}
		}
	}

	internal class Order
	{
		[JsonPropertyName("s")]
		public string Symbol { get; set; }

		[JsonPropertyName("i")]
		public int OrderId { get; set; }

		[JsonPropertyName("c")]
		public string ClientOrderId { get; set; }

		public override string ToString()
		{
			return $"{nameof(Symbol)}: {Symbol}, {nameof(OrderId)}: {OrderId}, {nameof(ClientOrderId)}: {ClientOrderId}";
		}
	}

	internal class ListStatus
	{
		[JsonPropertyName("e")]
		public string EventType { get; set; }

		[JsonPropertyName("E")]
		public long EventTime { get; set; }

		[JsonPropertyName("s")]
		public string Symbol { get; set; }

		[JsonPropertyName("g")]
		public int OrderListId { get; set; }

		[JsonPropertyName("c")]
		public string ContingencyType { get; set; }

		[JsonPropertyName("l")]
		public string ListStatusType { get; set; }

		[JsonPropertyName("L")]
		public string ListOrderStatus { get; set; }

		[JsonPropertyName("r")]
		public string ListRejectReason { get; set; }

		[JsonPropertyName("C")]
		public string ListClientOrderId { get; set; }

		[JsonPropertyName("T")]
		public long TransactionTime { get; set; }

		[JsonPropertyName("O")]
		public List<Order> Orders { get; set; }

		public override string ToString()
		{
			return
				$"{nameof(EventType)}: {EventType}, {nameof(EventTime)}: {EventTime}, {nameof(Symbol)}: {Symbol}, {nameof(OrderListId)}: {OrderListId}, {nameof(ContingencyType)}: {ContingencyType}, {nameof(ListStatusType)}: {ListStatusType}, {nameof(ListOrderStatus)}: {ListOrderStatus}, {nameof(ListRejectReason)}: {ListRejectReason}, {nameof(ListClientOrderId)}: {ListClientOrderId}, {nameof(TransactionTime)}: {TransactionTime}, {nameof(Orders)}: {Orders}";
		}
	}

	/// <summary>
	/// For Binance User Data stream (different from Balance): Balance Update occurs during the following:
	/// - Deposits or withdrawals from the account
	/// - Transfer of funds between accounts(e.g.Spot to Margin)
	/// </summary>
	internal class BalanceUpdate
	{
		[JsonPropertyName("e")]
		public string EventType { get; set; }

		[JsonPropertyName("E")]
		public long EventTime { get; set; }

		[JsonPropertyName("a")]
		public string Asset { get; set; }

		[JsonPropertyName("d")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal BalanceDelta { get; set; }

		[JsonPropertyName("T")]
		public long ClearTime { get; set; }
	}

	/// <summary>
	/// As part of outboundAccountPosition from Binance User Data Stream (different from BalanceUpdate)
	/// </summary>
	internal class Balance
	{
		[JsonPropertyName("a")]
		public string Asset { get; set; }

		[JsonPropertyName("f")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal Free { get; set; }

		[JsonPropertyName("l")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public decimal Locked { get; set; }

		public override string ToString()
		{
			return $"{nameof(Asset)}: {Asset}, {nameof(Free)}: {Free}, {nameof(Locked)}: {Locked}";
		}
	}

	/// <summary>
	/// outboundAccountPosition is sent any time an account balance has changed and contains
	/// the assets that were possibly changed by the event that generated the balance change.
	/// </summary>
	internal class OutboundAccount
	{
		[JsonPropertyName("e")]
		public string EventType { get; set; }

		[JsonPropertyName("E")]
		public long EventTime { get; set; }

		[JsonPropertyName("u")]
		public long LastAccountUpdate { get; set; }

		[JsonPropertyName("B")]
		public List<Balance> Balances { get; set; }

		/// <summary> convert the Balances list to a dictionary of total amounts </summary>
		public Dictionary<string, decimal> BalancesAsTotalDictionary
		{
			get
			{
				var dict = new Dictionary<string, decimal>();

				foreach (var balance in Balances)
				{
					dict.Add(balance.Asset, balance.Free + balance.Locked);
				}

				return dict;
			}
		}

		/// <summary> convert the Balances list to a dictionary of available to trade amounts </summary>
		public Dictionary<string, decimal> BalancesAsAvailableToTradeDictionary
		{
			get
			{
				var dict = new Dictionary<string, decimal>();

				foreach (var balance in Balances)
				{
					dict.Add(balance.Asset, balance.Free);
				}

				return dict;
			}
		}
	}
}
