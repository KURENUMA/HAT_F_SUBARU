using System.ComponentModel;

namespace HAT_F_api.CustomModels
{
    public class ViewReadySalesSearchCondition
    {
        /// <summary>物件コード</summary>
        public string 物件コード { get; set; }

        /// <summary>物件名</summary>
        public string 物件名 { get; set; }

        /// <summary>得意先コード</summary>
        public string 得意先コード { get; set; }

        /// <summary>得意先</summary>
        public string 得意先 { get; set; }

        /// <summary>発注先</summary>
        public short? 発注先 { get; set; }

        /// <summary>Hat注文番号</summary>
        public string Hat注文番号 { get; set; }

        /// <summary>営業担当者名</summary>
        public string 営業担当者名 { get; set; }

        /// <summary>受注合計金額</summary>
        public long? 受注合計金額 { get; set; }

        /// <summary>利率</summary>
        public decimal? 利率 { get; set; }

        /// <summary>伝票番号</summary>
        public string 伝票番号 { get; set; }

        /// <summary>伝票区分</summary>
        public string 伝票区分 { get; set; }

        /// <summary>納期</summary>
        public DateTime? 納期 { get; set; }

        /// <summary>仕入先コード</summary>
        public string 仕入先コード { get; set; }

        /// <summary>仕入先名</summary>
        public string 仕入先名 { get; set; }

        /// <summary>入荷日</summary>
        public DateTime? 入荷日 { get; set; }

        /// <summary>商品コード</summary>
        public string 商品コード { get; set; }

        /// <summary>商品名</summary>
        public string 商品名 { get; set; }

        /// <summary>数量</summary>
        public int? 数量 { get; set; }

        /// <summary>売上記号</summary>
        public string 売上記号 { get; set; }

        /// <summary>売上単価</summary>
        public decimal? 売上単価 { get; set; }

        /// <summary>売上額</summary>
        public decimal? 売上額 { get; set; }

        /// <summary>売上掛率</summary>
        public decimal? 売上掛率 { get; set; }

        /// <summary>仕入記号</summary>
        public string 仕入記号 { get; set; }

        /// <summary>仕入単価</summary>
        public decimal? 仕入単価 { get; set; }

        /// <summary>仕入額</summary>
        public decimal? 仕入額 { get; set; }

        /// <summary>仕入掛率</summary>
        public decimal? 仕入掛率 { get; set; }

        /// <summary>定価</summary>
        [DisplayName("定価")]
        public decimal? 定価 { get; set; }
    }
}