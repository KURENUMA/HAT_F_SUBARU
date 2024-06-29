﻿using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 支払先マスタ★
/// </summary>
public partial class PayeeMst0628old
{
    /// <summary>
    /// 支払先コード, (仕入先CD前4桁と一致）
    /// </summary>
    public string PayeeCode { get; set; }

    /// <summary>
    /// 支払先名
    /// </summary>
    public string PayeeName { get; set; }

    /// <summary>
    /// 支払先名カナ
    /// </summary>
    public string PayeeKana { get; set; }

    /// <summary>
    /// 支払先名略称★
    /// </summary>
    public string PayeeKanaShort { get; set; }

    /// <summary>
    /// 支店名(センター名)★
    /// </summary>
    public string PayeeBranchName { get; set; }

    /// <summary>
    /// 郵便番号
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    /// 都道府県
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// 住所１
    /// </summary>
    public string Address1 { get; set; }

    /// <summary>
    /// 住所２
    /// </summary>
    public string Address2 { get; set; }

    /// <summary>
    /// 住所3★追加
    /// </summary>
    public string Address3 { get; set; }

    /// <summary>
    /// TEL★追加
    /// </summary>
    public string Tel { get; set; }

    /// <summary>
    /// FAX★追加
    /// </summary>
    public string Fax { get; set; }

    /// <summary>
    /// FAX2★追加
    /// </summary>
    public string Fax2 { get; set; }

    /// <summary>
    /// 取引禁止フラグ
    /// </summary>
    public short? NoSalesFlg { get; set; }

    /// <summary>
    /// 雑区分
    /// </summary>
    public short? WideUseType { get; set; }

    /// <summary>
    /// 支払先締日,15:15日締め
    /// </summary>
    public short SupCloseDate { get; set; }

    /// <summary>
    /// 支払先支払月,0:当月,1:翌月,2:翌々月
    /// </summary>
    public short? SupPayMonths { get; set; }

    /// <summary>
    /// 支払先支払日,10:10日払い,99：末日
    /// </summary>
    public short? SupPayDates { get; set; }

    /// <summary>
    /// 支払方法区分,1:振込,2:手形,3:でんさい:デフォルトの支払方法
    /// </summary>
    public short? PayMethodType { get; set; }

    /// <summary>
    /// 与信限度額
    /// </summary>
    public long? MaxCredit { get; set; }

    /// <summary>
    /// 与信一時増加枠
    /// </summary>
    public long? TempCreditUp { get; set; }

    /// <summary>
    /// インボイス登録番号
    /// </summary>
    public string InvoiceRegistNumber { get; set; }

    /// <summary>
    /// 全銀協銀行コード★
    /// </summary>
    public string ABankCode { get; set; }

    /// <summary>
    /// 全銀協支店コード★
    /// </summary>
    public string ABankBlncCode { get; set; }

    /// <summary>
    /// 銀行名★
    /// </summary>
    public string BankName { get; set; }

    /// <summary>
    /// 銀行支店名★
    /// </summary>
    public string BankBlncName { get; set; }

    /// <summary>
    /// 銀行口座番号★
    /// </summary>
    public string BankNo { get; set; }

    /// <summary>
    /// 銀行口座種別,O:普通 C:当座★
    /// </summary>
    public string BankActType { get; set; }

    /// <summary>
    /// 銀行口座名義人★
    /// </summary>
    public string BankActName { get; set; }

    /// <summary>
    /// でんさいコード（利用者番号）★:Electronically Recorded Monetary Claims
    /// </summary>
    public string ErmcUserNo { get; set; }

    /// <summary>
    /// 削除日★追加
    /// </summary>
    public DateTime? DeleteDate { get; set; }

    /// <summary>
    /// 削除済★追加
    /// </summary>
    public bool? Deleted { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
