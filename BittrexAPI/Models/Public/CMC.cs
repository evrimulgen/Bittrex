namespace BittrexAPI.Models
{
    /// <summary>
    /// Retrieves the data from coinmarketcap
    /// </summary>
    public class CMC
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
        public decimal price_usd { get; set; }
        public decimal price_btc { get; set; }
        public decimal market_cap_usd { get; set; }
        public decimal available_supply { get; set; }
        public decimal total_supply { get; set; }
        public decimal max_supply { get; set; }
        public decimal percent_change_1h { get; set; }
        public decimal percent_change_24h { get; set; }
        public decimal percent_change_7d { get; set; }
        public string last_update { get; set; }
    }
}