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
    internal class HatF_PaymentMonthRepo
    {
        private static HatF_PaymentMonthRepo instance = null;
        private List<HatF_PaymentMonth> entities = new();
        private HatF_PaymentMonthRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_PaymentMonth>>(Resources.HatF_PaymentMonth);
        }
        public static HatF_PaymentMonthRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_PaymentMonthRepo();
            }
            return instance;
        }

        public List<HatF_PaymentMonth> Entities { get { return entities; } }
    }
}
