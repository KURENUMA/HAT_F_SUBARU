using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Models
{
    public class UserConfig
    {
        public string UserId { get; set; }
        public string Version { get; set; }
        public string SelectedPattern { get; set; }
        public Dictionary<string, PatternConfig> patterns;

    }


    /// <summary>
    /// 列の設定を格納するためのクラス
    /// </summary>
    public class ColumnSetting
    {
        public string ColumnName { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
    }


    public class PatternConfig
    {
        public List<string> ColumnOrder { get; set; }
        public List<ColumnSetting> ColumnSettings { get; set; }
    }
}
