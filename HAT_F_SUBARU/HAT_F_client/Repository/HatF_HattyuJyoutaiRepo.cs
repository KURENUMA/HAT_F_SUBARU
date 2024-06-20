using HatFClient.Models;
using HatFClient.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Repository
{
    internal class HatF_HattyuJyoutaiRepo
    {
        private static HatF_HattyuJyoutaiRepo instance = null;
        private List<HatF_HattyuJyoutai> entities = new();
        private HatF_HattyuJyoutaiRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_HattyuJyoutai>>(Resources.HatF_HattyuJyoutai);
        }
        public static HatF_HattyuJyoutaiRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_HattyuJyoutaiRepo();
            }
            return instance;
        }

        public List<HatF_HattyuJyoutai> Entities { get { return entities; } }
    }
}
