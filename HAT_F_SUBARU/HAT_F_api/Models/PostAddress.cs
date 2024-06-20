using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class PostAddress
{
    public long PostAddressId { get; set; }

    /// <summary>
    /// 全国地方公共団体コード（JIS X0401、X0402）
    /// </summary>
    public string GovernmentCode { get; set; }

    /// <summary>
    /// （旧）郵便番号（5桁）
    /// </summary>
    public string PostCode3 { get; set; }

    /// <summary>
    /// 郵便番号（7桁）
    /// </summary>
    public string PostCode7 { get; set; }

    /// <summary>
    /// 都道府県名（カナ）
    /// </summary>
    public string PrefecturesKana { get; set; }

    /// <summary>
    /// 市区町村名（カナ）
    /// </summary>
    public string MunicipalitiesKana { get; set; }

    /// <summary>
    /// 町域名
    /// </summary>
    public string TownAreaKana { get; set; }

    /// <summary>
    /// 都道府県名
    /// </summary>
    public string Prefectures { get; set; }

    /// <summary>
    /// 市区町村名
    /// </summary>
    public string Municipalities { get; set; }

    /// <summary>
    /// 町域名
    /// </summary>
    public string TownArea { get; set; }

    /// <summary>
    /// 一町域が二以上の郵便番号で表される場合の表示（「1」は該当、「0」は該当せず）
    /// </summary>
    public string MultiPostCodeTownArea { get; set; }

    /// <summary>
    /// 小字毎に番地が起番されている町域の表示（「1」は該当、「0」は該当せず）
    /// </summary>
    public string KoazaStreetAddress { get; set; }

    /// <summary>
    /// 丁目を有する町域の場合の表示（「1」は該当、「0」は該当せず）
    /// </summary>
    public string CyomeTownArea { get; set; }

    /// <summary>
    /// 一つの郵便番号で二以上の町域を表す場合の表示（「1」は該当、「0」は該当せず）
    /// </summary>
    public string MultiTownAreaPostCode { get; set; }

    /// <summary>
    /// 更新の表示（「0」は変更なし、「1」は変更あり、「2」廃止（廃止データのみ使用））
    /// </summary>
    public string UpdateType { get; set; }

    /// <summary>
    /// 変更理由　（「0」は変更なし、「1」市政・区政・町政・分区・政令指定都市施行、「2」住居表示の実施、「3」区画整理、「4」郵便区調整等、「5」訂正、「6」廃止（廃止データのみ使用））
    /// </summary>
    public string UpdateReason { get; set; }
}
