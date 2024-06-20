using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Repository
{
    internal class EmployeeRepo
    {
        private static EmployeeRepo _instance = null;

        public static EmployeeRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EmployeeRepo();
            }
            return _instance;
        }

        /// <summary>
        /// メール送信可能な社員情報
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetSendableEmplyeesAsync()
        {
            var response = await Program.HatFApiClient.GetAsync<List<Employee>>(ApiResources.HatF.Client.SendableEmployees);
            return ApiHelper.IsPositiveResponse(response) ? response.Data : new List<Employee>();
        }
    }
}
