using System;
using Newtonsoft.Json;

namespace BittrexAPI.Models
{
    public class ConfigReader<T>
    {
        public ConfigReader(T result)
        {
            Result = result;
        }
        
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
    }
    
    /// <summary>
    /// Deserialize the config.json file
    /// </summary>
    public class ConfigSerial
    {
        public string CoinName { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
