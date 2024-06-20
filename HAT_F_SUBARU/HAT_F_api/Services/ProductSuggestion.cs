using Azure.Identity;
using HAT_F_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Umayadia.Kana;

namespace HAT_F_api.Services
{

    public class ProductSuggestion
    {
        //サジェスト用商品名配列・遅延初期化（商品10万件程度の場合、初回10秒程度想定）
        private Lazy<string[]> _products = new Lazy<string[]>(() => Initialize());

        private static IConfiguration _configuration;
        private static NLog.ILogger _logger;
        private static readonly TimeSpan SuggestSelectTimeout = new TimeSpan(0, 3, 0);

        public ProductSuggestion(IConfiguration configuration, NLog.ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private static string[] Initialize()
        {
            // 仮実装
            string connectionString = _configuration!.GetConnectionString("DbConnectString")!; 
            using var sqlConnection = new SqlConnection(connectionString);

// TODO: 商品マスタのデータ量を調整したらTOPを外す
//#if DEBUG
            // ソート基準をHAT_SYOHINでなくCOM_SYOHINにするとキーではないため時間がかかるので注意
            string sql = $"SELECT TOP({100_000}) [COM_SYOHIN], [COM_SYOHIN_NAME], [COM_SYOHIN_KIKAKU], [SEL_NAME] FROM [COM_SYOHIN_MST] ORDER BY HAT_SYOHIN";
//#else
//            string sql = "SELECT [COM_SYOHIN], [COM_SYOHIN_NAME], [COM_SYOHIN_KIKAKU], [SEL_NAME] FROM [COM_SYOHIN_MST] ORDER BY HAT_SYOHIN";
//#endif

            using var cmd = new SqlCommand(sql, sqlConnection!);
            cmd.CommandTimeout = (int)SuggestSelectTimeout.TotalSeconds;
            sqlConnection.Open();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<string> products = new List<string>(5_000_000);

            System.Diagnostics.Debug.WriteLine($"【初期化】サジェスト用商品名全件取得 開始:{DateTime.Now:HH:mm:ss}");
            _logger.Log(NLog.LogLevel.Info, "【初期化】サジェスト用商品名全件取得 開始");

            StringBuilder productInfo = new StringBuilder(500);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // 以下文字列の組み立て
                //「商品コード<半スペース>商品名[規格]<TAB文字>SEL_NAME」
                // ・メンテナンス性がよくないがメモリ効率優先
                // ・クライアントにはTAB文字の前までを返す想定

                string productCode = (string)reader["COM_SYOHIN"];
                string productName = $"{reader["COM_SYOHIN_NAME"]}";        // DBNull ⇒ "" になります
                string productStandard = $"{reader["COM_SYOHIN_KIKAKU"]}";  // DBNull ⇒ "" になります
                if (!string.IsNullOrEmpty(productStandard)) { productName += "[" + productStandard + "]"; }

                productInfo.Clear();
                productInfo.AppendFormat("[{0}] {1}\t{2}",
                    productCode,
                    productName,
                    (string)reader["SEL_NAME"]);
                products.Add(productInfo.ToString());
            }

            sw.Stop();

            System.Diagnostics.Debug.WriteLine($"【初期化】サジェスト用商品名全件取得 終了 ({products.Count}件): {DateTime.Now:HH:mm:ss}");
            _logger.Log(NLog.LogLevel.Info, "【初期化】サジェスト用商品名全件取得 終了 ({0:#,##0}件)", products.Count);

            System.Diagnostics.Debug.WriteLine($"【初期化】サジェスト用商品名全件取得 所要時間: {sw.Elapsed.Seconds}s");
            _logger.Log(NLog.LogLevel.Info, "【初期化】サジェスト用商品名全件取得 所要時間: {0}s", sw.Elapsed.Seconds);

            return products.ToArray();
        }

        /// <summary>
        /// 半角カタカナ変換
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        private static string ToNarrowKatakana(string text)
            {
            // Strings.StrConvがAzure環境で使用できなかったので代替
            string narrowKatakana = KanaConverter.ToKatakana(text);
            narrowKatakana = KanaConverter.ToNarrow(narrowKatakana);
            return narrowKatakana;
        }

        /// <summary>
        /// 商品サジェスト用データの取得
        /// </summary>
        /// <param name="keyword">検索キーワード(中間一致)</param>
        /// <param name="rows">サジェストデータの最大取得件数</param>
        /// <returns></returns>
        public List<string> GetProductSuggestion(string keyword, int rows)
        {
            var searchSource = _products.Value;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string compareKeyword = ToNarrowKatakana(keyword);
            List<string> results = new List<string>();
            foreach (string item in searchSource)
            {
                // 検索用文字列開始位置
                int selNameStartPosition = item.IndexOf("\t") + 1;

                // 大文字小文字を区別せず検索
                string compareItem = ToNarrowKatakana(item);
                if (0 <= compareItem.IndexOf(compareKeyword ?? "", 0, StringComparison.CurrentCultureIgnoreCase))  // SEL_NAMEだけを検索対象にすると思った挙動にならないと思われる
                {
                    string returnText = item.Substring(0, selNameStartPosition - 1);
                    results.Add(returnText);
                    if (results.Count >= rows) 
                    {
                        results.Add($"《{rows}件以上該当したため省略されました》");
                        break;
                    }
                }
            }

            if (0 == results.Count)
            {
                results.Add("《候補が見つかりませんでした》");
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine($"商品検索所要時間 {sw.Elapsed.Milliseconds}ms/({keyword})");

            return results;
        }

    }

}
