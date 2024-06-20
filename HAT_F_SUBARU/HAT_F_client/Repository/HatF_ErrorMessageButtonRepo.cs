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
    internal class HatF_ErrorMessageButtonRepo
    {
        private static HatF_ErrorMessageButtonRepo instance = null;
        private List<HatF_ErrorMessage_Button> entities = new();
        private HatF_ErrorMessageButtonRepo()
        {
            this.entities = JsonConvert.DeserializeObject<List<HatF_ErrorMessage_Button>>(Resources.HatF_ErrorMessage_Button);
        }
        public static HatF_ErrorMessageButtonRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new HatF_ErrorMessageButtonRepo();
            }
            return instance;
        }

        public List<HatF_ErrorMessage_Button> Entities { get { return entities; } }
    }
}
