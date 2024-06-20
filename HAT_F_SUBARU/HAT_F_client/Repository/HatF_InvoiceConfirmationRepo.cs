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
    internal class HatF_InvoiceConfirmationRepo
    {
        private static HatF_InvoiceConfirmationRepo instance = null;
        private List<HatF_InvoiceConfirmation> entities = new();
        private HatF_InvoiceConfirmationRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_InvoiceConfirmation>>(Resources.HatF_InvoiceConfirmation);
        }
        public static HatF_InvoiceConfirmationRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_InvoiceConfirmationRepo();
            }
            return instance;
        }

        public List<HatF_InvoiceConfirmation> Entities { get { return entities; } }
    }
}
