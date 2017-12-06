using System;
using System.Threading.Tasks;
using BittrexAPI.Models;

namespace BittrexAPI.MainClient
{
    public class CalculationClient
    {
        private readonly BittrexClient _client;

        public CalculationClient(BittrexClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Calculate the transaction fee
        /// </summary>
        /// <param name="_coin">Name of the coin, e.g. BTC or LTC</param>
        public async Task<decimal> CalculateTransactionFee(string _coin)
        {
            decimal TxFee = 0;
            decimal lastPrice = 0;
            
            // Retrieve all coins from /Models/Public/Currency.cs
            var allCoins = await _client.GetCurrenies().ConfigureAwait(false);

            // Search the list of coins and match it with the _coin parameter
            foreach (var coin in allCoins.Result)
            {                
                if (coin.Currency == _coin)
                {
                    // What type of coin is this?
                    Console.WriteLine(coin.CoinType);
                    
                    // Check the marketworth in USD for the cointype
                    switch (coin.CoinType)
                    {
                        case "BITCOIN":
                            lastPrice = await CalculateFeeToUSD("USDT-BTC");
                            break;
                            
                        case "ETH":
                            lastPrice = await CalculateFeeToUSD("USDT-ETH");
                            break;
                    }
                    
                    // Set TxFee to the according fee of the coin
                    TxFee = coin.TxFee * lastPrice;
                }                
            }

            Console.WriteLine("The transactionfee for " + _coin + " is " + TxFee + " a piece");
            return TxFee;
        }

        /// <summary>
        /// Retrieves the Last price of the market
        /// </summary>
        /// <param name="_market">The market of the designated coin, e.g. USDT-BTC</param>
        private async Task<decimal> CalculateFeeToUSD(string _market)
        {
            decimal lastPrice = 0;
            ApiResult<Ticker> result;
            
            switch (_market)
            {                
                case "USDT-BTC":
                    result = await _client.GetTicker("USDT-BTC").ConfigureAwait(false);

                    lastPrice = result.Result.Last;
                    break;
                    
                case "USDT-ETH":
                    result = await _client.GetTicker("USDT-BTC").ConfigureAwait(false);

                    lastPrice = result.Result.Last;
                    break;
                    
                case "USDT-LTC":
                    result = await _client.GetTicker("USDT-LTC").ConfigureAwait(false);

                    lastPrice = result.Result.Last;
                    break;               
            }

            return lastPrice;
        }
    }
}
