using HatFClient.Models;
using HatFClient.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Repository
{
    internal class HatF_ErrorMessageFocusOutRepo
    {
        private static HatF_ErrorMessageFocusOutRepo instance = null;
        private List<HatF_ErrorMessage_FocusOut> entities = new ();

        private HatF_ErrorMessageFocusOutRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_ErrorMessage_FocusOut>>(Resources.HatF_ErrorMessage_FocusOut);
        }

        public static HatF_ErrorMessageFocusOutRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_ErrorMessageFocusOutRepo();
            }
            return instance;
        }

        public List<HatF_ErrorMessage_FocusOut> Entities { get { return entities; } }
    }
}
