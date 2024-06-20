using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewConstructionDetail
{
    public string 物件コード { get; set; }

    public string 検索キー { get; set; }

    public string チームcd { get; set; }

    public int? 担当社員id { get; set; }

    public string 物件名 { get; set; }

    public string 物件名フリガナ { get; set; }

    public DateTime? 引合日 { get; set; }

    public DateTime? 見積送付日 { get; set; }

    public DateTime? 注文書受領日 { get; set; }

    public DateTime? 注文請書受領日 { get; set; }

    public DateTime? 注文請書送付日 { get; set; }

    public DateTime? 受注対応完了日 { get; set; }

    public string 物件備考 { get; set; }

    public string 得意先コード { get; set; }

    public short? 受注状態 { get; set; }

    public int? 登録者社員id { get; set; }

    public short? 新設件数 { get; set; }

    public short? 改造件数 { get; set; }

    public short? 撤去件数 { get; set; }

    public string 現場郵便番号 { get; set; }

    public string 物件住所 { get; set; }

    public string 現場住所1 { get; set; }

    public string 現場住所2 { get; set; }

    public string 現場住所3 { get; set; }

    public string 現場tel { get; set; }

    public string 現場fax { get; set; }

    public string 建設会社名 { get; set; }

    public string 建設会社代表者名 { get; set; }

    public short? 建設種別 { get; set; }

    public short? 建設業種 { get; set; }

    public string 建設会社tel { get; set; }

    public string 建設会社fax { get; set; }

    public string 得意先名 { get; set; }

    public string 得意先住所 { get; set; }

    public string キーマンcd { get; set; }

    public string コメント { get; set; }

    public bool? セール { get; set; }

    public bool? Bs { get; set; }

    public bool? タイ { get; set; }

    public bool? 推薦物件 { get; set; }

    public bool? 未契約物件 { get; set; }

    public string 受注確度 { get; set; }

    public string ビル名等 { get; set; }
}
