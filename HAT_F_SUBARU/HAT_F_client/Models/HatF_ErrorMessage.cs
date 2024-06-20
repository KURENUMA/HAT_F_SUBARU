using Newtonsoft.Json;

namespace HatFClient.Models
{
    public class HatF_ErrorMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("explanation")]
        public string Explanation { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
