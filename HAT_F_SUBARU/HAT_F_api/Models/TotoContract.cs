using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// TOTO請書情報:読み込んだTOTO請書情報を格納するテーブル
/// </summary>
public partial class TotoContract
{
    /// <summary>
    /// 物件コード:取込した対象の物件コード
    /// </summary>
    public string ConstructionCode { get; set; }

    /// <summary>
    /// 受番:請書に記載されている7桁のコード
    /// </summary>
    public string RecvNo { get; set; }

    /// <summary>
    /// 内部番号:請書に記載されている6桁のコード
    /// </summary>
    public string InternalNo { get; set; }

    /// <summary>
    /// 届け先:直送の住所
    /// </summary>
    public string DeliveryAddress { get; set; }

    /// <summary>
    /// 宛先:直送の宛先
    /// </summary>
    public string RecipientAddress { get; set; }

    /// <summary>
    /// 現場名:物件に登録する現場とイコールになる
    /// </summary>
    public string SiteName { get; set; }

    /// <summary>
    /// 行番号:1～6までしかないはず
    /// </summary>
    public string LineNo { get; set; }

    /// <summary>
    /// 注番:各請書の項目毎に一意になる項目
    /// </summary>
    public string OrderNo { get; set; }

    /// <summary>
    /// 計上ステータス,0:未計上/1:計上済:他のステータスがあるかは要確認
    /// </summary>
    public short? AppropState { get; set; }

    /// <summary>
    /// 5桁コード
    /// </summary>
    public string Digit5Code { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProductCode { get; set; }

    /// <summary>
    /// 商品名
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// 仕入利率
    /// </summary>
    public decimal? PurchaseMargin { get; set; }

    /// <summary>
    /// 売単価
    /// </summary>
    public decimal? SellingPrice { get; set; }

    /// <summary>
    /// 仕入単価
    /// </summary>
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// 売合計金額
    /// </summary>
    public decimal? TotalSalesAmount { get; set; }

    /// <summary>
    /// 仕入合計金額
    /// </summary>
    public decimal? TotalPurchaseAmount { get; set; }

    /// <summary>
    /// 納期
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// 拠点:TOTO請書項目に存在したため念のため追加
    /// </summary>
    public DateTime? BaseDate { get; set; }

    /// <summary>
    /// 手配:TOTO請書項目に存在したため念のため追加
    /// </summary>
    public DateTime? ArrangementDate { get; set; }

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
