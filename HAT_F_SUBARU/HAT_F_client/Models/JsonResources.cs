using HatFClient.Properties;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HatFClient.Models
{
    /// <summary>JSONリソースファイルのオブジェクト</summary>
    internal class JsonResources
    {
        /// <summary>発注先種別</summary>
        public static List<CodeName<short>> SupplierTypes
            => JsonConvert.DeserializeObject<List<CodeName<short>>>(Resources.HatF_SupplierType);

        /// <summary>都道府県</summary>
        public static List<Prefecture> Prefectures
            => JsonConvert.DeserializeObject<List<Prefecture>>(Resources.prefectures);

        /// <summary>締日</summary>
        public static List<CodeName<short?>> CloseDates
            => JsonConvert.DeserializeObject<List<CodeName<short?>>>(Resources.HatF_CloseDate);

        /// <summary>取引禁止フラグ</summary>
        public static List<CodeName<short>> NoSalesFlags
            => JsonConvert.DeserializeObject<List<CodeName<short>>>(Resources.HatF_NoSalesFlg);

        /// <summary>支払月</summary>
        public static List<CodeName<short>> PayMonths
            => JsonConvert.DeserializeObject<List<CodeName<short>>>(Resources.HatF_PayMonths);

        /// <summary>支払日</summary>
        public static List<CodeName<short>> PayDates
            => JsonConvert.DeserializeObject<List<CodeName<short>>>(Resources.HatF_PayDates);

        /// <summary>支払方法（区分）</summary>
        public static List<CodeName<short>> PayMethodTypes
            => JsonConvert.DeserializeObject<List<CodeName<short>>>(Resources.HatF_PayMethodType);

        /// <summary>売上調整区分</summary>
        public static Dictionary<short, string> SalesAdjustmentCategories
            => JsonConvert.DeserializeObject<List<CodeName<short>>>(Resources.HatF_SalesAdjustmentCategory)
                    .ToDictionary(x => x.Code, x => x.Name);
        // TODO JsonファイルをデータソースとするXXXRepoの内容もここに統合したい
    }
}
