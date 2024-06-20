using HatFClient.Models;
using HatFClient.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Repository
{
    internal class HatF_OrderFlagRepo
    {
        private static HatF_OrderFlagRepo instance = null;
        private List<HatF_OrderFlag> entities = new();
        private HatF_OrderFlagRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_OrderFlag>>(Resources.HatF_OrderFlag);
        }
        public static HatF_OrderFlagRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_OrderFlagRepo();
            }
            return instance;
        }

        public List<HatF_OrderFlag> Entities { get { return entities; } }
    }
}
