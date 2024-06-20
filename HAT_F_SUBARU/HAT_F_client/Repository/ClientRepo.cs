using HAT_F_api.Models;
using HatFClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HAT_F_api.CustomModels;
using HatFClient.Constants;
using HatFClient.HatApi.Models;
using System.Threading;
using HatFClient.Common;

namespace HatFClient.Repository {
    public class ClientRepo {
        private static Lazy<ClientRepo> _instance  = new Lazy<ClientRepo>(() => 
        {
            // シングルトン＆遅延初期化＆スレッドセーフ
            var cr = new ClientRepo();
            cr.loadInit();
            return cr;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public ClientInit Options {  get; set; } 

        private ClientRepo() { }

        public static ClientRepo GetInstance()
            => _instance.Value;

        private void loadInit() {
            Options = Program.HatFApiClient.Get<ClientInit>(ApiResources.HatF.Client.Init).Data;
        }

        public async Task<List<Announcement>> getAnnouncements() {
            var response = await Program.HatFApiClient.GetAsync<List<Announcement>>(ApiResources.HatF.Announcement.View);
            return ApiHelper.IsPositiveResponse(response) ? response.Data : new List<Announcement>();
        }
    }
}
