using Newtonsoft.Json;

namespace BittrexAPI.Models
{
    /// <summary>
    /// Bittrex API calls return a succes boolean, a message and a result
    /// This class filters those API call returns
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        public ApiResult(bool success, string message, T result)
        {
            Success = success;
            Message = message;
            Result = result;
        }
        
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
    }
}