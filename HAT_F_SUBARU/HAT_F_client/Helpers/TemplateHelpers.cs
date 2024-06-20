using C1.Win.C1FlexGrid;
using HatFClient.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Helpers
{
    public static class TemplateHelpers
    {
        /// <summary>
        /// データマッピングを設定
        /// 表示したいデータは本個所で設定する
        /// 列名 | 幅 | データの型 | 文字位置 | フォント | 設定するデータ
        /// ラムダ式を使用して設定→ラムダ式：簡潔な関数（メソッド）の表現方法
        /// 例：data => ((ProjectDatum)data).noはdataオブジェクトを受け取り、ProjectDatumにキャストしてnoプロパティの値を返す動作となる
        /// </summary>
        public static readonly List<ColumnMappingConfig> TemplateColumnConfigs = new List<ColumnMappingConfig>
        {
            new ColumnMappingConfig("物件コード", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).物件コード), 
                "物件コード"),

            new ColumnMappingConfig("物件名", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).物件名), 
                "物件名"),

            new ColumnMappingConfig("得意先コード", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).得意先コード), 
                "得意先コード"),

            new ColumnMappingConfig("得意先", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).得意先), 
                "得意先"),

            new ColumnMappingConfig("発注先", 120, typeof(short), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).発注先), 
                "発注先"),

            new ColumnMappingConfig("発注先種別名", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).発注先種別名), 
                "発注先種別名"),

            new ColumnMappingConfig("Hat注文番号", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).Hat注文番号), 
                "Hat注文番号"),

            new ColumnMappingConfig("営業担当者名", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).営業担当者名), 
                "営業担当者名"),

            new ColumnMappingConfig("受注合計金額", 150, typeof(long), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).受注合計金額), 
                "受注合計金額"),

            new ColumnMappingConfig("利率", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).利率), 
                "利率"),

            new ColumnMappingConfig("伝票番号", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).伝票番号), 
                "伝票番号"),

            new ColumnMappingConfig("伝票区分", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).伝票区分), 
                "伝票区分"),

            new ColumnMappingConfig("伝票区分名", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).伝票区分名), 
                "伝票区分名"),

            new ColumnMappingConfig("納期", 120, typeof(DateTime), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).納期), 
                "納期"),

            new ColumnMappingConfig("仕入先コード", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).仕入先コード), 
                "仕入先コード"),

            new ColumnMappingConfig("仕入先名", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).仕入先名), 
                "仕入先名"),

            new ColumnMappingConfig("入荷日", 120, typeof(DateTime), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).入荷日), 
                "入荷日"),

            new ColumnMappingConfig("商品コード", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).商品コード), 
                "商品コード"),

            new ColumnMappingConfig("商品名", 150, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).商品名), 
                "商品名"),

            new ColumnMappingConfig("数量", 120, typeof(int), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).数量), 
                "数量"),

            new ColumnMappingConfig("売上記号", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).売上記号), 
                "売上記号"),

            new ColumnMappingConfig("売上単価", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).売上単価), 
                "売上単価"),

            new ColumnMappingConfig("売上額", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).売上額), 
                "売上額"),

            new ColumnMappingConfig("売上掛率", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).売上掛率), 
                "売上掛率"),

            new ColumnMappingConfig("仕入記号", 120, typeof(string), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).仕入記号), 
                "仕入記号"),

            new ColumnMappingConfig("仕入単価", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).仕入単価), 
                "仕入単価"),

            new ColumnMappingConfig("仕入額", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).仕入額), 
                "仕入額"),

            new ColumnMappingConfig("仕入掛率", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).仕入掛率), 
                "仕入掛率"),

            new ColumnMappingConfig("定価", 120, typeof(decimal), TextAlignEnum.CenterCenter, 
                new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((HAT_F_api.Models.ViewReadySale)data).定価), 
                "定価"),
        };
    }
}
