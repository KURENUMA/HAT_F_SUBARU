namespace HAT_F_api.CustomModels
{
    public class FosJyuchuSearchCondition
    {
        /// <summary>
        /// 取得件数
        /// </summary>
        public int rows { get; set; } = 200;

        /// <summary> チームCD</summary>
        public string TeamCd { get; set; }
        /// <summary> 得意先CD</summary>
        public string TokuCd { get; set; }
        /// <summary> キーマンCD</summary>
        public string KmanCd { get; set; }
        /// <summary> 仕入先CD</summary>
        public string ShiresakiCd { get; set; }
        /// <summary> 受注者</summary>
        public string Jyu2 { get; set; }
        /// <summary> 入力者</summary>
        public string Nyu2 { get; set; }
        /// <summary> 現場CD</summary>
        public string GenbaCd { get; set; }
        /// <summary> 現場名</summary>
        public string GenbaName { get; set; }
        /// <summary> 客先注番</summary>
        public string CustOrderNo { get; set; }
        /// <summary> HAT注番</summary>
        public string HatOrderNo { get; set; }
        /// <summary> 発注方法（0:計上のみ, 1:FAX, 2:メール, 3:手動, 4:オンライン, 5:プリントアウト, 6:FTP発注）</summary>
        public string Hkbn { get; set; }
        /// <summary> 伝票No. </summary>
        public string DenNo { get; set; }
        /// <summary> 受注番号</summary>
        public string OrderNo { get; set; }
        /// <summary> 状態（1:発注前, 2:手配中・回答待, 3:ACOS済, 4:削除 6:請書処理済）</summary>
        public string State { get; set; }
        /// <summary> 商品コード</summary>
        public string SyohinCd { get; set; }
        /// <summary> 商品名</summary>
        public string SyohinName { get; set; }
        /// <summary> 受注日(From)</summary>
        public DateTime? RecYmdFrom { get; set; }
        /// <summary>  受注日(TO)</summary>
        public DateTime? RecYmdTo { get; set; }
        /// <summary> 納期(From)</summary>
        public DateTime? NoukiFrom { get; set; }
        /// <summary> 納期(TO)</summary>
        public DateTime? NoukiTo { get; set; }
        /// <summary> 受注区分（1:通常受注, 2:見積受注, 3:ＯＰＳ, 4:HOPE, 5:請書取込, 6:エプコ取込, 7:新ＯＰＳ, 8:ＯＣＲ）</summary>
        public string OrderFlag { get; set; }
        /// <summary> OPSNo.</summary>
        public string OpsOrderNo { get; set; }
        /// <summary> OPS入力日(From)</summary>
        public DateTime? OpsRecYMDFrom { get; set; }
        /// <summary> OPS入力日(To)</summary>
        public DateTime? OpsRecYMDTo { get; set; }
        /// <summary> エプコ管理番号</summary>
        public string EpukoKanriNo { get; set; }


        ///// <summary> 物件名/ Property Name </summary>
        //public string SBukken { get; set; }
        ///// <summary> 伝票発注状態（0:未確定　1:発注済 　2:一部回答済　3:回答済　4:照合済）/ Check order status (0: unconfirmed 1: ordered 2: partially answered 3: answered 4: collated) </summary>
        //public string SDenState { get; set; }
        ///// <summary> 受注日/ Order date </summary>
        //public string SRecYmd { get; set; }
        ///// <summary> 納期/ Delivery date </summary>
        //public string SNouki { get; set; }
        //public string SDesq { get; set; }
        ///// <summary> ＯＰＳ入力日/ OPS input date </summary>
        //public string SOpsRecYMD { get; set; }
        //public string SLine { get; set; }
        ///// <summary> 見積番号/ Estimated number </summary>
        //public string SEstimateNo { get; set; }
        ///// <summary> 見積日(From) / Estimated date (From) </summary>STeamCd
        //public string SEstInpDateFrom { get; set; }
        ///// <summary> 見積日(TO) / Estimated date (TO) </summary>
        //public string SEstInpDateTo { get; set; }
        ///// <summary> 得意先CD（必須) / CD first (required)</summary>
        //public string SToukuiCd { get; set; }
    }
}
