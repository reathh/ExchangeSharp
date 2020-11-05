using System;

namespace TestConsole
{
	using System.Threading.Tasks;
	using ExchangeSharp;

	class Program
	{
		static async Task Main(string[] args)
		{
			await TestBitMEXTicker();
			Console.WriteLine("Hello World!");
		}

		static async Task TestBitMEXTicker()
		{
			var api = new ExchangeBitMEXAPI()
			{
				PublicApiKey = "ETGe46gWSIAxB6u8lTpzo9E2".ToSecureString(),
				PrivateApiKey = "keSSndN337T_BABe_f0QhK7GEAx1ZByz-Ou9Sn9yTgxm61QK".ToSecureString()
			};

			var ticker = await api.GetTickerAsync("xbtusd");
			Console.WriteLine(ticker);
		}
	}
}
