namespace HAT_F_api.CustomModels
{
    public class FosJyuchuSearch
    {
        /// <summary>
        /// 削除フラグ（0:未削除, 1:削除済）
        /// </summary>
        public string DelFlg { get; set; }

        /// <summary>
        /// 発注方法（0:計上のみ, 1:FAX, 2:メール, 3:手動, 4:オンライン, 5:プリントアウト, 6:FTP発注）
        /// </summary>
        public string Hkbn { get; set; }

        /// <summary>
        /// 伝No
        /// </summary>
        public string DenNo { get; set; }

        /// <summary>
        /// 内部No.
        /// </summary>
        public string Dseq { get; set; }

        /// <summary>
        /// 得意先
        /// </summary>
        public string TokuiCd { get; set; }

        /// <summary>
        /// 客先注番（客注）
        /// </summary>
        public string CustOrderno { get; set; }

        /// <summary>
        /// 現場（現場CD）
        /// </summary>
        public string GenbaCd { get; set; }

        /// <summary>
        /// 仕入先（仕入先CD）
        /// </summary>
        public string ShiresakiCd { get; set; }

        /// <summary>
        /// 受注日
        /// </summary>
        public DateTime? RecYmd { get; set; }

        /// <summary>
        /// HAT注番
        /// </summary>
        public string HatOrderNo { get; set; }

        /// <summary>
        /// 受注者名
        /// </summary>
        public string Jyu2Name { get; set; }

        /// <summary>
        /// 状態（1:発注前, 2:手配中・回答待, 3:ACOS済, 4:削除 6:請書処理済）
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 受注区分（受区）（1:通常受注, 2:見積受注, 3:ＯＰＳ, 4:HOPE, 5:請書取込, 6:エプコ取込, 7:新ＯＰＳ, 8:ＯＣＲ）
        /// </summary>
        public string OrderFlag { get; set; }

        /// <summary>
        /// OPSNo.
        /// </summary>
        public string OpsOrderNo { get; set; }

        /// <summary>
        /// OPS入力日
        /// </summary>
        public DateTime? OpsRecYmd { get; set; }

        /// <summary>
        /// 希望納期
        /// </summary>
        public DateTime? Nouki { get; set; }

        /// <summary>
        /// 頁
        /// </summary>
        public string DenSort { get; set; }


        /// <summary>
        /// 《画面対応なし》
        /// </summary>
        public string SaveKey { get; set; }

        /// <summary>
        /// 伝票状態（1:発注前, 2～4:手配中・回答待, 3:ACOS済）
        /// </summary>
        public string DenState { get; set; }

        /// <summary>
        /// チームCD
        /// </summary>
        public string TeamCd { get; set; }

        /// <summary>
        /// キーマンCD
        /// </summary>
        public string KmanCd { get; set; }

        /// <summary>
        /// 現場名
        /// </summary>
        public string Bukken { get; set; }

        /// <summary>
        /// 《画面対応なし》
        /// </summary>
        public string OrderNo { get; set; }


        /// <summary>
        /// 受注者
        /// </summary>
        public string Jyu2 { get; set; }
        /// <summary>
        /// 受注者CD
        /// </summary>
        public string Jyu2Cd { get; set; }
        /// <summary>
        /// 受注者ID
        /// </summary>
        public int? Jyu2Id { get; set; }

        /// <summary>
        /// 商品CD
        /// </summary>
        public string SyohinCd { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string SyohinName { get; set; }
    }
}
