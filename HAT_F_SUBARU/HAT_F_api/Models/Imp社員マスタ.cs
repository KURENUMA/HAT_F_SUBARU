using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class Imp社員マスタ
{
    public string 社員id連番自動生成 { get; set; }

    public string 社員コード { get; set; }

    public string 社員名 { get; set; }

    public string 社員名カナ { get; set; }

    public string 社員指定タグ { get; set; }

    public string パスワード { get; set; }

    public string 部門コード { get; set; }

    public string 電話番号 { get; set; }

    public string Fax番号 { get; set; }

    public string 開始日 { get; set; }

    public string 職種コード雇用形態 { get; set; }

    public string 承認権限コード { get; set; }

    public string 役職 { get; set; }
}
