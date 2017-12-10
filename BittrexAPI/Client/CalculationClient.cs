using System;
using System.Threading.Tasks;
using BittrexAPI.Models;

namespace BittrexAPI.Client
{
    public class CalculationClient
    {
        private readonly Bittrex _bittrex;

        public CalculationClient(Bittrex bittrex)
        {
            _bittrex = bittrex;
        }
        
        /// <summary>
        /// This method will calculate an amount of coins in a certain market to USD 
        /// </summary>
        /// <param name="coin">The coin parameter requires a marketname, e.g. EMC2</param>
        /// <param name="amount">The amount parameter requires an amount in decimal, e.g. 1.342387429</param>
        /// <returns></returns>
        public async Task<decimal> CalculateToDollar(string coin, decimal amount)
        {
            var baseCurrency = "";
            var marketName = "";
            
            // GetMarkets() has a MarketCurrency (which is equal to the coin parameter) and BaseCurrency, use this to get creative!
            var getMarkets = await _bittrex.GetMarkets().ConfigureAwait(false);

            // Cycle throught all markets to compare the marketcurrency to the coin parameter. If found, asign it's baseCurrency and marketName
            foreach (var item in getMarkets.Result)
            {                
                if (item.MarketCurrency == coin)
                {
                    Console.WriteLine(coin + " has been found, settings it's basecurrency as " + item.BaseCurrency + ". It's MarketName is " + item.MarketName);
                    baseCurrency = item.BaseCurrency;
                    marketName = item.MarketName;
                }
            }
            
            // Use ticker with marketName. That way we should be able to get it's LastPrice
            var ticker = await _bittrex.GetTicker(marketName).ConfigureAwait(false);
            Console.WriteLine("The last price of " + coin + " is " + ticker.Result.Last + " " + baseCurrency + "/piece");
            
            // Do a calculation, the lastprice times the amount of this methods parameter
            var totalAmount = amount * ticker.Result.Last;
            Console.WriteLine("You have " + amount + " of " + coin + " which is worth " + totalAmount + " " + baseCurrency);

            // If it's basecurrency is USDT, we can return the totalamount right here and cancel further operations
            if (baseCurrency == "USDT")
            {
                // Empty line for clearer format
                Console.WriteLine();
                return totalAmount;
            }
            
            // Calculate the last price of the basecurrency in USD
            decimal lastPrice = await CalculateLastPrice(baseCurrency).ConfigureAwait(false);

            // Calculate 
            decimal totalWorthInUSD = totalAmount * lastPrice;

            Console.WriteLine(amount + " of " + coin + " is worth " + totalWorthInUSD + " USD\n");
            return totalWorthInUSD;
        }

        /// <summary>
        /// Calculates the Lastprice of the basecurrency in USD
        /// </summary>
        /// <param name="baseCurrency">The basecurrency of the coin, which is either BTC, ETH or USDT</param>
        /// <returns>The last price of the coins basecurrency in BTC</returns>
        private async Task<decimal> CalculateLastPrice(string baseCurrency)
        {
            decimal lastPrice = 0;
            ApiResult<Ticker> result;            

            switch (baseCurrency)
            {
                case "BTC":
                    result = await _bittrex.GetTicker("USDT-BTC").ConfigureAwait(false);
                    lastPrice = result.Result.Last;
                    break;
                    
                case "ETH":
                    result = await _bittrex.GetTicker("USDT-ETH").ConfigureAwait(false);
                    lastPrice = result.Result.Last;
                    break;                                        
                    
                default:
                    Console.WriteLine("Something went wrong in the switch statement in CalculationClient. Maybe the baseCurrency (" + baseCurrency + ") hasn't been implemented yet?");
                    break;
            }

            return lastPrice;
        }
    }
}
