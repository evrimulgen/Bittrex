using System;
using System.Threading.Tasks;

namespace BittrexAPI.MainClient
{
    public class CalculationClient
    {
        private readonly Client _client;

        public CalculationClient(Client client)
        {
            _client = client;
        }
        
        // Calculate amount in BTC to USD
        public async Task<decimal> CalculateToDollar(decimal amount)
        {
            // Retrieve lastPrice of the USDT-BTC market
            var ticker = await _client.GetTicker("USDT-BTC").ConfigureAwait(false);
            decimal lastPrice = ticker.Result.Last;

            Console.WriteLine("Lastprice of USDT-BTC: " + lastPrice);

            var result = amount * lastPrice;
            
            return result;
        }                       
    }
}
