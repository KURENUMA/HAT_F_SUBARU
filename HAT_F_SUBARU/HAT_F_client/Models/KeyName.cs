using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Models
{
    internal class KeyName
    {
        [JsonProperty("key")]
        public short? Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
