using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 受注ヘッダー
/// </summary>
public partial class FosJyuchuSearchResult
{
    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string SaveKey { get; set; } = null;

    public string DenSort { get; set; } = null;
    /// <summary>
    /// 発注
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
    /// 客注
    /// </summary>
    public string CustOrderno { get; set; }

    /// <summary>
    /// 現場
    /// </summary>
    public string GenbaCd { get; set; }

    /// <summary>
    /// 仕入
    /// </summary>
    public string ShiresakiCd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? RecYmd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string HatOrderNo { get; set; }
    /// <summary>
    /// 受発注者
    /// </summary>
    public string Jyu2 { get; set; }
    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Jyu2Cd { get; set; }

    /// <summary>
    /// 伝区
    /// </summary>
    public string DenFlg { get; set; }
    
    /// <summary>
    /// 受区
    /// </summary>
    public string OrderFlag { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderNo { get; set; }



    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Bukken { get; set; }


    /// <summary>
    /// 納日
    /// </summary>
    public DateTime? Nouki { get; set; }



    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsOrderNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? OpsRecYmd { get; set; }
}

