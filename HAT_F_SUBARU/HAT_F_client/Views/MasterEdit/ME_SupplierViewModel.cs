using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Views.MasterEdit
{
    internal class ME_SupplierViewModel
    {
        /// <summary>
        /// 仕入先コード
        /// </summary>
        [DisplayName("仕入先コード*")]
        public string SupCode { get; set; }

        /// <summary>
        /// 仕入先枝番
        /// </summary>
        [DisplayName("仕入先枝番")]
        public short SupSubNo { get; set; }

        /// <summary>
        /// 仕入先名
        /// </summary>
        [DisplayName("仕入先名*")]
        public string SupName { get; set; }

        /// <summary>
        /// 仕入先名カナ
        /// </summary>
        [DisplayName("仕入先名カナ")]
        public string SupKana { get; set; }

        /// <summary>
        /// 仕入先担当者名
        /// </summary>
        [DisplayName("仕入先担当者名")]
        public string SupEmpName { get; set; }

        /// <summary>
        /// 仕入先部門名
        /// </summary>
        [DisplayName("仕入先部門名")]
        public string SupDepName { get; set; }

        /// <summary>
        /// 仕入先郵便番号
        /// </summary>
        [DisplayName("仕入先郵便番号")]
        public string SupZipCode { get; set; }

        /// <summary>
        /// 仕入先都道府県
        /// </summary>
        [DisplayName("仕入先都道府県")]
        public string SupState { get; set; }

        /// <summary>
        /// 仕入先住所１
        /// </summary>
        [DisplayName("仕入先住所１")]
        public string SupAddress1 { get; set; }

        /// <summary>
        /// 仕入先住所２
        /// </summary>
        [DisplayName("仕入先住所２")]
        public string SupAddress2 { get; set; }

        /// <summary>
        /// 仕入先電話番号
        /// </summary>
        [DisplayName("仕入先電話番号")]
        public string SupTel { get; set; }

        /// <summary>
        /// 仕入先FAX番号
        /// </summary>
        [DisplayName("仕入先FAX番号")]
        public string SupFax { get; set; }

        /// <summary>
        /// 仕入先メールアドレス
        /// </summary>
        [DisplayName("仕入先メールアドレス")]
        public string SupEmail { get; set; }

        /// <summary>
        /// 仕入先締日,15:15日締め
        /// </summary>
        [DisplayName("仕入先締日")]
        public short? SupCloseDate { get; set; }

        [DisplayName("仕入先締日")]
        public short? SupCloseDate_Text { get; set; }

        /// <summary>
        /// 仕入先支払月,0:当月,1:翌月,2:翌々月
        /// </summary>
        [DisplayName("仕入先支払月")]
        public short? SupPayMonths { get; set; }

        [DisplayName("仕入先支払月")]
        public short? SupPayMonths_Text { get; set; }

        /// <summary>
        /// 仕入先支払日,10:10日払い,99：末日
        /// </summary>
        [DisplayName("仕入先支払日")]
        public short? SupPayDates { get; set; }

        [DisplayName("仕入先支払日")]
        public short? SupPayDates_Text { get; set; }

        /// <summary>
        /// 支払方法区分,1:振込,2:手形
        /// </summary>
        [DisplayName("支払方法区分")]
        public short? PayMethodType { get; set; }

        [DisplayName("支払方法区分")]
        public short? PayMethodType_Text { get; set; }

        /// <summary>
        /// 発注先種別,null/0:未設定 1:橋本本体 2:橋本本体以外
        /// </summary>
        [DisplayName("発注先種別")]
        public short? SupplierType { get; set; }

        [DisplayName("発注先種別")]
        public short? SupplierType_Text { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        [DisplayName("作成日時")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 作成者名
        /// </summary>
        [DisplayName("作成者名")]
        public int? Creator { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [DisplayName("更新日時")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 更新者名
        /// </summary>
        [DisplayName("更新者名")]
        public int? Updater { get; set; }

        [DisplayName("あああ")]

        public string SupPayMonthsText { get; set; }
    }

}
