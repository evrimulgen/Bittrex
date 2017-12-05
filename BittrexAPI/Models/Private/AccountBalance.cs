namespace BittrexAPI.Models
{
    /// <summary>
    /// Retrieves the user's balance
    /// </summary>
    public class AccountBalance
    {
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal Available { get; set; }
        public decimal Pending { get; set; }
        public string CryptoAddress { get; set; }
        public bool Requested { get; set; }
        // What datatype is Uuid???
        public string Uuid { get; set; }
    }
}
