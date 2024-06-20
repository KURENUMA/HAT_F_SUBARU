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
    internal class HatF_PaymentDayRepo
    {
        private static HatF_PaymentDayRepo instance = null;
        private List<HatF_PaymentDay> entities = new();
        private HatF_PaymentDayRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_PaymentDay>>(Resources.HatF_PaymentDay);
        }
        public static HatF_PaymentDayRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_PaymentDayRepo();
            }
            return instance;
        }

        public List<HatF_PaymentDay> Entities { get { return entities; } }
    }
}
