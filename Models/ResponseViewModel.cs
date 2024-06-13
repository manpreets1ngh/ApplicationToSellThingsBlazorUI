using Newtonsoft.Json;

namespace ApplicationToSellThing.BlazorUI.Models
{
    public class ResponseViewModel<T>
    {
        [JsonProperty("statuscode")]
        public int StatusCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T? Data { get; set; }

        [JsonProperty("items")]
        public List<T>? Items { get; set; }
    }
}
