using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static C1.Util.Win.Win32;

namespace HatFClient.Common
{
    /// <summary>
    /// Application Insights
    /// </summary>
    internal class ApplicationInsightsHelper
    {
        /// <summary>
        /// Application Insightsのシングルトン保持とスレッドセーフ遅延初期化
        /// </summary>
        private static Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => InitializeTelemetryClient());

        /// <summary>
        /// Application InsightsのTelemetryClient
        /// </summary>
        public static TelemetryClient TelemetryClient => _telemetryClient.Value;

        public ApplicationInsightsHelper()
        {

        }

        /// <summary>
        /// Application Insights クライアントの初期化
        /// </summary>
        private static TelemetryClient InitializeTelemetryClient()
        {
            // ログ出力先Application Insightsの接続文字列
            string connectionString = HatFConfigReader.GetConnectionString("ApplicationInsights");

            TelemetryConfiguration telemetryConfiguration = new TelemetryConfiguration() { 
                ConnectionString = connectionString,
#if DEBUG
                TelemetryChannel = new InMemoryChannel(),     // 即出力されますが再試行機能がないので開発用としておきます
#endif
            };

            // ログ出力レベルを調整
            SeverityLevel severityLevel = (SeverityLevel)Enum.Parse(typeof(SeverityLevel), HatFConfigReader.GetAppSetting("ApplicationInsights:SeverityLevel"));
            var builder = telemetryConfiguration.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
            builder.Use((next) => new LogLevelFilter(next, severityLevel));
            builder.Build();

            TelemetryClient telemetryClient = new TelemetryClient(telemetryConfiguration);

            // ★ログ出力レベルの参考

            // デバッグ用の詳細出力（開発中に情報を増やしたいとき）：SeverityLevel.Verbose
            // ApplicationInsightsHelper.TelemetryClient.TrackTrace("デバッグ用", SeverityLevel.Verbose);
            // ※「System.Diagnostics.Debug.WriteLine("xxxxx");」を使えばVisual Studioの「出力」に出るのでそちらでもOK

            // 有用そうな情報を残すとき：SeverityLevel.Information
            // ApplicationInsightsHelper.TelemetryClient.TrackTrace("○○処理開始(終了)", SeverityLevel.Information);

            // 一時的なエラーなど、すぐの対応は不要なもの：SeverityLevel.Warning
            // ApplicationInsightsHelper.TelemetryClient.TrackTrace("通信リトライn回目", SeverityLevel.Warning);

            // すぐに調査すべきエラー：SeverityLevel.Error
            // ApplicationInsightsHelper.TelemetryClient.TrackTrace("設定ファイルに必須項目 xxx が見つからない", SeverityLevel.Error);

            // 致命的なエラー：SeverityLevel.Critical（基本的にはSeverityLevel.Errorでよいかも）
            // ApplicationInsightsHelper.TelemetryClient.TrackTrace("何か致命的なエラー", SeverityLevel.Critical);

            return telemetryClient;
        }
    }
}
