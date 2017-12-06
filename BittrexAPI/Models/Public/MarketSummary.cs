using System;

namespace BittrexAPI.Models
{
    /// <summary>
    /// Retrieves the market summary
    /// </summary>
    public class MarketSummary
    {
        public string MarketName { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Last { get; set; }
        public decimal BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public decimal PrevDay { get; set; }
        public DateTime Created { get; set; }
        public string DisplayMarketName { get; set; }
    }
}