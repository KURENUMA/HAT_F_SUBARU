using Newtonsoft.Json;

namespace HatFClient.Models
{
    public class HatF_HattyuJyoutai
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
