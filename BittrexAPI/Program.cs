using System.Threading.Tasks;
using BittrexAPI.MainClient;

namespace BittrexAPI
{
    public class Program
    {
        public static async Task Main()
        {           
            // Our apiKey and apiSecret variables
            string apiKey = "";
            string apiSecret = "";
            
            // Set up the two clients: one for the operations (such as buy/sell) and one to calculate the margins
            var bittrexClient = new Client(apiKey, apiSecret);           
            var calculationClient = new CalculationClient(bittrexClient);            

            await calculationClient.CalculateTransactionFee("ETH");
        }
    }
}
