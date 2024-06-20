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
    internal class HatF_PaymentClassificationRepo
    {
        private static HatF_PaymentClassificationRepo instance = null;
        private List<HatF_PaymentClassification> entities = new();
        private HatF_PaymentClassificationRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_PaymentClassification>>(Resources.HatF_PaymentClassification);
        }
        public static HatF_PaymentClassificationRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_PaymentClassificationRepo();
            }
            return instance;
        }

        public List<HatF_PaymentClassification> Entities { get { return entities; } }
    }
}
