using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Models
{
    /// <summary>
    /// Hat_F_api/Models/ViewInvoiceのドメインモデル
    /// </summary>
    internal class ViewInvoice
    {
        public int 物件コード { get; set; }

        public string 物件名 { get; set; }

        public string 得意先コード { get; set; }

        public string 得意先 { get; set; }

        public int 請求金額合計 { get; set; }

        public int 消費税総額 { get; set; }

        public string 営業担当 { get; set; }

        public string ステータス { get; set; }

        public string 顧客支払月 { get; set; }

        public string 顧客支払日 { get; set; }

        public string 顧客支払方法 { get; set; }
    }
}
