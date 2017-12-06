namespace BittrexAPI.Models
{
    /// <summary>
    /// Recieves ticker information from bittrex
    /// </summary>
    public class Ticker
    {
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
    }
}