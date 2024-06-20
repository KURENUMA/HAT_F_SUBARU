using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 受注ヘッダー
/// </summary>
public partial class FosJyuchuH
{
    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string SaveKey { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderNo { get; set; }

    /// <summary>
    /// 《ラベルなし》「発注前」
    /// </summary>
    public string OrderState { get; set; }

    /// <summary>
    /// 伝No
    /// </summary>
    public string DenNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string DenSort { get; set; }

    /// <summary>
    /// 《ラベルなし》「発注前」
    /// </summary>
    public string DenState { get; set; }

    /// <summary>
    /// 《ラベルなし》出荷伝票印刷済
    /// </summary>
    public bool? DenShippingPrinted { get; set; }

    /// <summary>
    /// 伝区
    /// </summary>
    public string DenFlg { get; set; }

    /// <summary>
    /// 伝票チェック
    /// </summary>
    public string DenValidCheckState { get; set; }

    /// <summary>
    /// 伝票チェック日時
    /// </summary>
    public DateTime? DenValidCheckDate { get; set; }

    /// <summary>
    /// 倉庫ステータス
    /// </summary>
    public string WhStatus { get; set; }

    /// <summary>
    /// 見積番号
    /// </summary>
    public string EstimateNo { get; set; }

    /// <summary>
    /// 見積番号
    /// </summary>
    public string EstCoNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Bukken { get; set; }

    /// <summary>
    /// 販課
    /// </summary>
    public string TeamCd { get; set; }

    /// <summary>
    /// 《画面対応なし》担当(CD)
    /// </summary>
    public string TantoCd { get; set; }

    /// <summary>
    /// 《画面対応なし》担当(名)
    /// </summary>
    public string TantoName { get; set; }

    /// <summary>
    /// 受発注者
    /// </summary>
    public string Jyu2 { get; set; }

    /// <summary>
    /// 《画面対応なし》 (受発注者/社員番号)
    /// </summary>
    public string Jyu2Cd { get; set; }

    /// <summary>
    /// 《画面対応なし》 (受発注者/社員ID)
    /// </summary>
    public int? Jyu2Id { get; set; }

    /// <summary>
    /// 入力者
    /// </summary>
    public string Nyu2 { get; set; }

    /// <summary>
    /// 《画面対応なし》 (入力者/社員番号)
    /// </summary>
    public string Nyu2Cd { get; set; }

    /// <summary>
    /// 《画面対応なし》 (入力者/社員ID)
    /// </summary>
    public int? Nyu2Id { get; set; }

    /// <summary>
    /// H注番 (HAT-F注文番号)
    /// </summary>
    public string HatOrderNo { get; set; }

    /// <summary>
    /// 客注
    /// </summary>
    public string CustOrderno { get; set; }

    /// <summary>
    /// 得意先(CD)
    /// </summary>
    public string TokuiCd { get; set; }

    /// <summary>
    /// 得意先(名)
    /// </summary>
    public string TokuiName { get; set; }

    /// <summary>
    /// 担(CD)(キーマン)
    /// </summary>
    public string KmanCd { get; set; }

    /// <summary>
    /// 担(名)(キーマン)
    /// </summary>
    public string KmanName { get; set; }

    /// <summary>
    /// 現場
    /// </summary>
    public string GenbaCd { get; set; }

    /// <summary>
    /// 納日
    /// </summary>
    public DateTime? Nouki { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? HatNyukabi { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string BukkenExp { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Sale1Flag { get; set; }

    /// <summary>
    /// 決済
    /// </summary>
    public string Kessai { get; set; }

    /// <summary>
    /// 来勘
    /// </summary>
    public string Raikan { get; set; }

    /// <summary>
    /// 工事店(CD)
    /// </summary>
    public string KoujitenCd { get; set; }

    /// <summary>
    /// 工事店(名)
    /// </summary>
    public string KoujitenName { get; set; }

    /// <summary>
    /// 倉庫(CD)
    /// </summary>
    public string SokoCd { get; set; }

    /// <summary>
    /// 倉庫(名)
    /// </summary>
    public string SokoName { get; set; }

    /// <summary>
    /// 社内備考
    /// </summary>
    public string NoteHouse { get; set; }

    /// <summary>
    /// 受区
    /// </summary>
    public string OrderFlag { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? RecYmd { get; set; }

    /// <summary>
    /// 仕入(CD)
    /// </summary>
    public string ShiresakiCd { get; set; }

    /// <summary>
    /// 仕入(名)
    /// </summary>
    public string ShiresakiName { get; set; }

    /// <summary>
    /// 発注
    /// </summary>
    public string Hkbn { get; set; }

    /// <summary>
    /// 依頼
    /// </summary>
    public string Sirainm { get; set; }

    /// <summary>
    /// FAX
    /// </summary>
    public string Sfax { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string SmailAdd { get; set; }

    /// <summary>
    /// 発注時メモ
    /// </summary>
    public string OrderMemo1 { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderMemo2 { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderMemo3 { get; set; }

    /// <summary>
    /// 区分
    /// </summary>
    public string Nohin { get; set; }

    /// <summary>
    /// 運賃
    /// </summary>
    public string Unchin { get; set; }

    /// <summary>
    /// 扱便(CD)
    /// </summary>
    public string Bincd { get; set; }

    /// <summary>
    /// 扱便(名)
    /// </summary>
    public string Binname { get; set; }

    /// <summary>
    /// 送元
    /// </summary>
    public string OkuriFlag { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string ShireKa { get; set; }

    /// <summary>
    /// 内部No.
    /// </summary>
    public string Dseq { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderDenNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string MakerDenNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string IpAdd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? InpDate { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? UpdDate { get; set; }

    /// <summary>
    /// 回答者
    /// </summary>
    public string AnswerName { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string DelFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string AnswerConfirmFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string SansyoDseq { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string ReqBiko { get; set; }

    /// <summary>
    /// 電話連絡済
    /// </summary>
    public string TelRenrakuFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》 (請書取込フラグ)
    /// </summary>
    public string UkeshoFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》 (エプコ管理No)
    /// </summary>
    public string EpukoKanriNo { get; set; }

    /// <summary>
    /// 発注先種別,null/0:未設定 1:橋本本体 2:橋本本体以外
    /// </summary>
    public short? SupplierType { get; set; }

    /// <summary>
    /// 物件コード
    /// </summary>
    public string ConstructionCode { get; set; }

    /// <summary>
    /// 入荷日
    /// </summary>
    public DateTime? ArrivalDate { get; set; }

    /// <summary>
    /// 出荷日
    /// </summary>
    public DateTime? ShippedDate { get; set; }

    /// <summary>
    /// 納入日
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string RecvGenbaCd { get; set; }

    /// <summary>
    /// 宛先1
    /// </summary>
    public string RecvName1 { get; set; }

    /// <summary>
    /// 宛先2
    /// </summary>
    public string RecvName2 { get; set; }

    /// <summary>
    /// TEL
    /// </summary>
    public string RecvTel { get; set; }

    /// <summary>
    /// 〒
    /// </summary>
    public string RecvPostcode { get; set; }

    /// <summary>
    /// 住所1
    /// </summary>
    public string RecvAdd1 { get; set; }

    /// <summary>
    /// 住所2
    /// </summary>
    public string RecvAdd2 { get; set; }

    /// <summary>
    /// 住所3
    /// </summary>
    public string RecvAdd3 { get; set; }

    /// <summary>
    /// OPSNo.
    /// </summary>
    public string OpsOrderNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? OpsRecYmd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsHachuAdr { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsBin { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsHachuName { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.受注番号》
    /// </summary>
    public string GOrderNo { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.受注日》
    /// </summary>
    public DateTime? GOrderDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.部門開始日》
    /// </summary>
    public DateTime? GStartDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.顧客コード》
    /// </summary>
    public string GCustCode { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.顧客枝番》
    /// </summary>
    public string GCustSubNo { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.受注金額合計》
    /// </summary>
    public long? GOrderAmnt { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.消費税金額》
    /// </summary>
    public long? GCmpTax { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.作成日時》
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.作成者》
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.更新日時》
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.更新者》
    /// </summary>
    public int? Updater { get; set; }
}
