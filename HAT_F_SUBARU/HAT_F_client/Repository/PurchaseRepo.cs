using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Constants;
using HatFClient.Views.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static HatFClient.Views.CorrectionDelivery.CorrectionDeliveryDetail;

namespace HatFClient.Repository
{
    public class PurchaseRepo
    {
        private static PurchaseRepo instance = null;

        private PurchaseRepo() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static PurchaseRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new PurchaseRepo();
            }
            return instance;
        }

        /// <summary>仕入詳細情報を検索</summary>
        /// <param name="condition">検索条件</param>
        /// <returns>仕入詳細情報</returns>
        public async Task<ApiResponse<List<ViewPurchaseBillingDetail>>> GetDetailAsync(ViewPurchaseBillingDetailCondition condition)
        {
            var parameters = new Dictionary<string, object>();
            foreach (var p in condition.GetType().GetProperties())
            {
                if (p.GetValue(condition) != null)
                {
                    parameters.Add(p.Name, p.GetValue(condition));
                }
            }
            return (await Program.HatFApiClient.GetAsync<List<ViewPurchaseBillingDetail>>(ApiResources.HatF.Client.PurchaseBillingDetails, parameters));
        }

        public async Task<List<ViewPurchaseReceivingDetail>> GetDetail(ViewPurchaseReceivingDetail condition)
        {
            var parameters = new Dictionary<string, object>();
            foreach (var p in condition.GetType().GetProperties())
            {
                if (p.GetValue(condition) != null)
                {
                    parameters.Add(p.Name, p.GetValue(condition));
                }
            }
            var result = (await Program.HatFApiClient.GetAsync<List<ViewPurchaseReceivingDetail>>(ApiResources.HatF.Client.PurchaseReceivingDetails, parameters));
            if (result != null)
            {
                return result.Data;
            }
            return null;
        }

        public async Task<List<ViewSalesReturnReceiptDetail>> GetDetail(ViewSalesReturnReceiptDetail condition)
        {
            var parameters = new Dictionary<string, object>()
            {
                        { "hatOrderNo", condition.Hat注文番号},
                        { "denNo", condition.伝票番号}
            };
            var result = (await Program.HatFApiClient.GetAsync<List<ViewSalesReturnReceiptDetail>>(ApiResources.HatF.Client.SalesReturnReceiptDetail, parameters));
            if (result != null)
            {
                return result.Data;
            }
            return null;
        }

        public async Task<List<ViewSalesRefundDetail>> GetDetail(ViewSalesRefundDetail condition)
        {
            var parameters = new Dictionary<string, object>()
            {
                        { "hatOrderNo", condition.Hat注文番号},
                        { "denNo", condition.伝票番号}
            };
            var result = (await Program.HatFApiClient.GetAsync<List<ViewSalesRefundDetail>>(ApiResources.HatF.Client.SalesRefundDetail, parameters));
            if (result != null)
            {
                return result.Data;
            }
            return null;
        }

        public async Task<List<ViewPurchaseSalesCorrection>> GetDetail(ViewPurchaseSalesCorrection condition)
        {
            var parameters = new Dictionary<string, object>()
            {
                        { "hatOrderNo", condition.Hat注文番号}
            };

            var result = (await Program.HatFApiClient.GetAsync<List<ViewPurchaseSalesCorrection>>(ApiResources.HatF.Client.PurchaseSalesCorrection, parameters));
            if (result != null)
            {
                return result.Data;
            }
            return null;
        }

        public async Task<List<ViewSalesReturnDetail>> GetDetail(ViewSalesReturnDetail condition)
        {
            var parameters = new Dictionary<string, object>()
            {
                        { "hatOrderNo", condition.Hat注文番号},
                        { "denNo", condition.伝票番号}
            };
            var result = (await Program.HatFApiClient.GetAsync<List<ViewSalesReturnDetail>>(ApiResources.HatF.Client.SalesReturnDetail, parameters));
            if (result != null)
            {
                return result.Data;
            }
            return null;
        }
        public async Task<List<CheckableViewCorrectionDeliveryDetail>> GetDetail(string compCode, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var parameters = new Dictionary<string, object>()
            {
                        { "compCode", compCode},
                        { "fromDate", fromDate},
                        { "toDate", toDate}
            };
            var result = (await Program.HatFApiClient.GetAsync<List<CheckableViewCorrectionDeliveryDetail>>(ApiResources.HatF.Client.CorrectionDeliveryDetail, parameters));
            if (result != null)
            {
                return result.Data;
            }
            return null;
        }
    }
}
