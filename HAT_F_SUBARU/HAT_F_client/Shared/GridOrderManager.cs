using C1.Win.C1FlexGrid;
using DocumentFormat.OpenXml.Presentation;
using HatFClient.CustomModels;
using HatFClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;

namespace HatFClient.Shared
{
    /// <summary>
    /// グリッド情報を管理するクラス
    /// JSONを参照してグリッドの項目順、表示非表示、幅を取得できる
    /// JSONにキーを増やす場合、本クラスにAPIを追加して参照することを想定
    /// </summary>
    public class GridOrderManager
    {
        private readonly string _jsonFilePath = null;
        public string FilePath => _jsonFilePath;
        public readonly List<ColumnMappingConfig> _initialColumnConfigs;
        private UserConfig _userPreference = null;

        public event System.EventHandler<EventArgs> PatternChanged;

        /// <remarks>
        /// 「OnPatternChanged(EventArgs.Empty)」でイベントを発生させます
        /// </remarks>
        public virtual void OnPatternChanged(System.EventArgs e)
        {
            PatternChanged?.Invoke(this, e);
        }

        public string SelectedPattern => _userPreference.SelectedPattern;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="jsonFileName">JSONファイルの名前</param>
        /// <param name="initialColumnConfigs">初期の列設定</param>
        public GridOrderManager(List<ColumnMappingConfig> initialColumnConfigs = null)
        {
            _initialColumnConfigs = initialColumnConfigs;
            //if (initialColumnConfigs != null)
            //{
            //    _userPreference = GenerateNewJsonData(initialColumnConfigs);
            //}
        }

        //private UserConfig ParseFromFile()
        //{
        //    return JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(_jsonFilePath));
        //}

        /// <summary>
        /// 指定されたパターン名が存在するかどうかを判定する
        /// </summary>
        /// <param name="patternName">チェックするパターン名</param>
        /// <returns>存在する場合はtrue、そうでない場合はfalse</returns>
        //public bool DoesPatternExist(string patternName)
        //{
        //    return _userPreference.patterns.ContainsKey(patternName);
        //}

        //public string[] GetColumnOrderForSelectedPattern()
        //{
        //    var patternName = _userPreference.SelectedPattern;
        //    if (!DoesPatternExist(patternName))
        //    {
        //        throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
        //    }
        //    return _userPreference.patterns[patternName].ColumnOrder.ToArray();
        //}

        /// <summary>
        /// 指定されたパターンに基づいて列の順序を取得
        /// </summary>
        /// <param name="patternName">列の順序を取得するパターン名</param>
        /// <returns>列の順序</returns>
        /// 
        //public string[] GetColumnOrderForPattern(string patternName)
        //{
        //    if (!DoesPatternExist(patternName))
        //    {
        //        throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
        //    }

        //    return _userPreference.patterns[patternName].ColumnOrder.ToArray();
        //}

        /// <summary>
        /// 指定されたパターンに基づいて列の設定を取得
        /// </summary>
        /// <param name="patternName">列の設定を取得するパターン名</param>
        /// <returns>列の設定のリスト</returns>
        //public List<ColumnSetting> GetColumnSettingsForPattern(string patternName)
        //{
        //    if (!DoesPatternExist(patternName))
        //    {
        //        throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
        //    }

        //    return ParseFromFile().patterns[patternName].ColumnSettings;
        //}

        //public List<ColumnSetting> GetColumnSettingsForSelectedPattern()
        //{
        //    string patternName = _userPreference.SelectedPattern;
        //    if (!DoesPatternExist(patternName))
        //    {
        //        throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
        //    }
        //    return ParseFromFile().patterns[patternName].ColumnSettings;

        //}

        /// <summary>
        /// 表示・非表示状態の取得
        /// </summary>
        //public bool GetVisibilityForColumn(string patternName, string columnName)
        //{
        //    if (_userPreference.patterns.TryGetValue(patternName, out PatternConfig pattern))
        //    {
        //        return pattern.ColumnSettings.First(x => x.ColumnName == columnName).Visible;
        //    }
        //    return true;
        //}

        /// <summary>
        /// 指定された設定に基づいてグリッドの列設定を適用
        /// </summary>
        /// <param name="grid">列設定を適用するグリッド</param>
        /// <param name="settings">列設定のリスト</param>
        //public void ApplyColumnSettingsToGrid(C1FlexGrid grid, List<ColumnSetting> settings)
        //{
        //    foreach (var setting in settings)
        //    {
        //        var col = grid.Cols[setting.ColumnName];
        //        col.Width = setting.Width;
        //        col.Visible = setting.Visible;
        //    }
        //}



        /// <summary>
        /// 指定されたパターンのcolumnOrderを更新する
        /// </summary>
        /// <param name="patternName">更新対象のパターン名</param>
        /// <param name="newOrder">新しいcolumnOrderの配列</param>
        //public void UpdateColumnOrderForPattern(string patternName, string[] newOrder)
        //{
        //    if (_userPreference.patterns.TryGetValue(patternName, out PatternConfig pattern))
        //    {
        //        pattern.ColumnOrder = newOrder.ToList();
        //    }
        //}

        /// <summary>
        /// 指定されたパターンのcolumnSettingsの表示・非表示情報を更新する
        /// </summary>
        /// <param name="patternName">更新対象のパターン名</param>
        /// <param name="visibilityInfo">表示・非表示情報の辞書</param>
        //public void UpdateColumnVisibilityForPattern(string patternName, Dictionary<string, bool> visibilityInfo)
        //{
        //    if (_userPreference.patterns.TryGetValue(patternName, out PatternConfig pattern))
        //    {
        //        foreach (var item in pattern.ColumnSettings)
        //        {
        //            var columnName = item.ColumnName;
        //            if (visibilityInfo.ContainsKey(columnName))
        //            {
        //                item.Visible = visibilityInfo[columnName];
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// ColumnMappingConfigのリストを基に新しいJSONデータを生成する
        /// </summary>
        /// <param name="columnConfigs">ColumnMappingConfigのリスト</param>
        /// <returns>生成されたJObjectオブジェクト</returns>
        //    private UserConfig GenerateNewJsonData(List<ColumnMappingConfig> columnConfigs)
        //    {
        //        var patternNames = new[] { "PatternA", "PatternB", "PatternC" };
        //        var _conf = new UserConfig
        //        {
        //            //UserId = State.LoginUser.login_id,
        //            Version = "1.1",
        //            SelectedPattern = patternNames[0],
        //            patterns = new Dictionary<string, PatternConfig>()

        //        };

        //            _conf.patterns.Add(patternName, new PatternConfig
        //            {
        //                ColumnOrder = columnConfigs.Select(config => config.Caption).ToList(),
        //                ColumnSettings = columnConfigs.Select(config => new Models.ColumnSetting
        //                {
        //                    ColumnName = config.Caption,
        //                    Width = config.Width,
        //                    Visible = true
        //                }).ToList()
        //            });

        //        return _conf;
        //    }

        /// <summary>
        /// 表のカスタム情報に沿ってグリッド列を設定します
        /// </summary>
        /// <param name="grid">対象のC1FlexGrid</param>
        /// <param name="configs">表のカスタム設定</param>
        /// <param name="containsUnselectedColumns">カスタム設定で選択されなかった列をC1FlexGridに含めるか。含める場合、非表示列として追加されます。</param>
        public void InitializeGridColumns(C1FlexGrid grid, BindingList<ColumnInfo> configs, bool containsUnselectedColumns)
        {
            //var grid = projectGrid1.c1FlexGrid1;

            grid.AutoGenerateColumns = false;

            var dt = grid.DataSource as DataTable;
            if (null == dt)
            {
                // データバインド前はconfigのみで構築
                grid.SuspendLayout();
                grid.Cols.Count = configs.Count + 1;

                // 一番左の灰色の列
                grid.Cols[0].Caption = "";
                grid.Cols[0].Width = 30;

                int columnIndexOffset = 1;
                for (int i = 0; i < configs.Count; i++)
                {
                    var config = configs[i];
                    var col = grid.Cols[i + columnIndexOffset];

                    System.Diagnostics.Debug.WriteLine(col.Name);

                    if (string.IsNullOrEmpty(col.Caption))
                    {
                        col.Caption = config.Label;
                    }

                    col.Width = config.Width;
                    col.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                    col.Name = config.VarName;
                    col.AllowFiltering = AllowFiltering.None;
                }

                grid.ResumeLayout();
                return;
            }


            grid.SuspendLayout();
            grid.Cols.Count = containsUnselectedColumns ? (dt.Columns.Count + 1) : (configs.Count + 1);

            // 一番左の灰色の列
            grid.Cols[0].Caption = "";
            grid.Cols[0].Width = 30;

            // データ表示列
            int colIndex = 1;
            foreach (DataColumn dataColumn in dt.Columns)
            {
                Column gridCol = grid.Cols[colIndex];
                gridCol.Name = dataColumn.ColumnName;

                var config = configs.Where(x => x.VarName == dataColumn.ColumnName).FirstOrDefault();
                if (config == null)
                {
                    if (!containsUnselectedColumns)
                    {
                        continue;
                    }

                    string caption = gridCol.Caption;
                    if (string.IsNullOrEmpty(caption))
                    {
                        caption = dataColumn.ColumnName;
                    }
                    gridCol.Caption = $"{caption}(非表示)";
                    gridCol.Visible = false;
                }
                else
                {
                    string caption = gridCol.Caption;
                    if (string.IsNullOrEmpty(caption))
                    {
                        caption = config.Label;                        
                    }
                    else
                    {
                        //if (config.VarName != config.Label)
                        {
                            caption = config.Label;
                        }
                    }
                    gridCol.Caption = caption;

                    gridCol.Width = config.Width;
                    gridCol.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                    gridCol.AllowFiltering = AllowFiltering.None;
                }

                colIndex++;
            }

            //for (int i = 0; i < configs.Count; i++)
            List<ColumnInfo> orderdConfigs = configs.ToList();
            if (orderdConfigs.Max(x => x.Index) > 0)
            {
                // Indexは後付けなので値がある場合のみソート
                orderdConfigs = orderdConfigs.OrderBy(x => x.Index).ToList();
            }

            int colOrderIndex = 1;
            foreach (ColumnInfo config in orderdConfigs)
            {
                //var config = configs[i];
                Column col = grid.Cols[config.VarName];
                if (col != null)
                {
                    grid.Cols[config.VarName].Move(colOrderIndex++);
                }
            }
            grid.ResumeLayout();
        }

    }
}
