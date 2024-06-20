using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.HatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HatFClient.Repository {
    public class LoginRepo {
        private static  LoginRepo instance = null;

        public HatFLoginResult CurrentUser { get; set; }

        private LoginRepo() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static LoginRepo GetInstance() {
            if (instance == null) {
                instance = new LoginRepo();
            }
            return instance;
        }

        public async Task<HatFLoginResult> DoLogin(string loginId, string password) {
            var response = await Program.HatFApiClient.LoginAsync(loginId, password);
            return response.Data;
        }

        public Task<ApiResponse<HatFLoginResult>> DoAuth(string employeeCode, string password)
            => Program.HatFApiClient.LoginAsync(employeeCode, password);
    }
}
