using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 物件情報★:物件情報を格納するテーブル
/// </summary>
public partial class Construction
{
    /// <summary>
    /// 物件コード
    /// </summary>
    public string ConstructionCode { get; set; }

    /// <summary>
    /// チームCD:物件王に合わせて項目追加
    /// </summary>
    public string TeamCd { get; set; }

    /// <summary>
    /// 担当社員ID:物件王に合わせて項目追加
    /// </summary>
    public int? EmpId { get; set; }

    /// <summary>
    /// 物件名
    /// </summary>
    public string ConstructionName { get; set; }

    /// <summary>
    /// 物件名フリガナ
    /// </summary>
    public string ConstructionKana { get; set; }

    /// <summary>
    /// 検索キー:ユーザー自由項目
    /// </summary>
    public string SearchKey { get; set; }

    /// <summary>
    /// 推薦物件:推薦物件かどうか
    /// </summary>
    public bool? Recommended { get; set; }

    /// <summary>
    /// 物件備考
    /// </summary>
    public string ConstructtonNotes { get; set; }

    /// <summary>
    /// 推薦備考:推薦物件の備考を入力するカラム
    /// </summary>
    public string RecommendComment { get; set; }

    /// <summary>
    /// 未契約物件:契約単価が決定する前に受注したかどうか
    /// </summary>
    public bool? Uncontracted { get; set; }

    /// <summary>
    /// 受注確度
    /// </summary>
    public string OrderConfidence { get; set; }

    /// <summary>
    /// 引合日:引合を受けた日
    /// </summary>
    public DateTime? InquiryDate { get; set; }

    /// <summary>
    /// 見積送付日:見積書を得意先に提出した日
    /// </summary>
    public DateTime? EstimateSendDate { get; set; }

    /// <summary>
    /// 注文書受領日:受注日とイコール
    /// </summary>
    public DateTime? OrderRceiptDate { get; set; }

    /// <summary>
    /// 注文請書受領日:注文書とセットで送られてくる
    /// </summary>
    public DateTime? OrderContractRceiptDate { get; set; }

    /// <summary>
    /// 注文請書送付日:社印を押して得意先に送付した日
    /// </summary>
    public DateTime? OrderContractSendDate { get; set; }

    /// <summary>
    /// 受注対応完了日:受注対応が完了した日
    /// </summary>
    public DateTime? OrderCompeltedDate { get; set; }

    /// <summary>
    /// 得意先コード:得意先マスタ (取引先マスタ)の取引先コード
    /// </summary>
    public string TokuiCd { get; set; }

    /// <summary>
    /// キーマンCD:物件王に合わせて項目追加
    /// </summary>
    public string KmanCd { get; set; }

    /// <summary>
    /// 受注状態,0:引合/1:見積作成/2:見積提出/3:受注済/4:完了
    /// </summary>
    public short? OrderState { get; set; }

    /// <summary>
    /// 登録者社員ID
    /// </summary>
    public int? RegistorEmpId { get; set; }

    /// <summary>
    /// 新設件数
    /// </summary>
    public short? NewInstallCount { get; set; }

    /// <summary>
    /// 改造件数
    /// </summary>
    public short? RenovationCount { get; set; }

    /// <summary>
    /// 撤去件数
    /// </summary>
    public short? RemovalCount { get; set; }

    /// <summary>
    /// 物件住所:NACSS取込で使用
    /// </summary>
    public string PropertyAddress { get; set; }

    /// <summary>
    /// 現場郵便番号:一貫化の工事店に該当する箇所
    /// </summary>
    public string RecvPostcode { get; set; }

    /// <summary>
    /// 現場住所1:一貫化に流すため同じ体系にする
    /// </summary>
    public string RecvAdd1 { get; set; }

    /// <summary>
    /// 現場住所2:一貫化に流すため同じ体系にする
    /// </summary>
    public string RecvAdd2 { get; set; }

    /// <summary>
    /// 現場住所3:一貫化に流すため同じ体系にする
    /// </summary>
    public string RecvAdd3 { get; set; }

    /// <summary>
    /// ビル名等:物件王に合わせて項目追加
    /// </summary>
    public string BuildingNameEtc { get; set; }

    /// <summary>
    /// 現場TEL
    /// </summary>
    public string RecvTel { get; set; }

    /// <summary>
    /// 現場FAX
    /// </summary>
    public string RecvFax { get; set; }

    /// <summary>
    /// 建設会社名
    /// </summary>
    public string ConstructorName { get; set; }

    /// <summary>
    /// 建設会社代表者名
    /// </summary>
    public string ConstructorRepName { get; set; }

    /// <summary>
    /// 建設種別,0:マンション:物件王に合わせて項目追加
    /// </summary>
    public short? ConstructorType { get; set; }

    /// <summary>
    /// 建設業種,0:大手サブコン:物件王に合わせて項目追加
    /// </summary>
    public short? ConstructorIndustry { get; set; }

    /// <summary>
    /// 建設会社TEL
    /// </summary>
    public string ConstructorTel { get; set; }

    /// <summary>
    /// 建設会社FAX
    /// </summary>
    public string ConstructorFax { get; set; }

    /// <summary>
    /// コメント:備考を入力するカラム
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// セール
    /// </summary>
    public bool? SalesEvent { get; set; }

    /// <summary>
    /// BS
    /// </summary>
    public bool? BalanceSheet { get; set; }

    /// <summary>
    /// タイ
    /// </summary>
    public bool? Thailand { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public int? Updater { get; set; }
}
