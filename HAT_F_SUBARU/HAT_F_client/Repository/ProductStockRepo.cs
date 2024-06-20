using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Net.Http;
using HAT_F_api.CustomModels;
using HatFClient.Constants;
using HAT_F_api.Models;

namespace HatFClient.Repository
{
    public class ProductStockRepo
    {
        private static  ProductStockRepo instance = null;


        private ProductStockRepo() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ProductStockRepo GetInstance() {
            if (instance == null) {
                instance = new ProductStockRepo();
            }
            return instance;
        }

        public async Task<List<ProductStock>> GetAll() {
            return (await Program.HatFApiClient.GetAsync<List<ProductStock>>(ApiResources.HatF.Client.ProductStock)).Data;
        }

        public async Task<ApiResponse<List<ProductStock>>> GetAllApiResponseAsync()
        {
            return (await Program.HatFApiClient.GetAsync<List<ProductStock>>(ApiResources.HatF.Client.ProductStock));
        }

        public async Task<ApiResponse<int>> GetAllCountApiResponseAsync()
        {
            return (await Program.HatFApiClient.GetAsync<int>(ApiResources.HatF.Client.ProductStockCount));
        }

        public async Task<ProductStockSummary> GetSummary() {
            return (await Program.HatFApiClient.GetAsync<ProductStockSummary>(ApiResources.HatF.Client.ProductStockSummary)).Data;
        }

        public async Task<ApiResponse<ViewProductStock>> GetSummaryApiResponseAsync()
        {
            return (await Program.HatFApiClient.GetAsync<ViewProductStock>(ApiResources.HatF.Client.ProductStockSummary));
        }


        public async Task<List<ProductStock>> getPage(int pageNo, int numRows)
        {
            var parameter = new Dictionary<string, object>()
            {
                {"page", pageNo },
                {"rows", numRows },
            };
            return (await Program.HatFApiClient.GetAsync<List<ProductStock>>(ApiResources.HatF.Client.ProductStock, parameter)).Data;
        }

        public async Task<ApiResponse<List<ViewProductStock>>> getPageApiResponseAsync(int pageNo, int numRows)
        {
            var parameter = new Dictionary<string, object>()
            {
                {"page", pageNo },
                {"rows", numRows },
            };
            return (await Program.HatFApiClient.GetAsync<List<ViewProductStock>>(ApiResources.HatF.Client.ProductStock, parameter));
        }
    }
}
