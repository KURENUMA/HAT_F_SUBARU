using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using HatFClient.Common;
using HatFClient.HatApi;
using Microsoft.ApplicationInsights.DataContracts;

namespace HatFClient
{
    internal static class Program
    {
        /// <summary>
        /// TelemetryClient.Flush 後の待機時間
        /// </summary>
        /// <remarks>
        /// https://devblogs.microsoft.com/premier-developer/application-insights-use-case-for-telemetryclient-flush-calls/
        /// </remarks>
        private static TimeSpan _telemetryFlushingWait = new TimeSpan(0, 0, 5);

        /// <summary>橋本総業一貫化APIクライアント</summary>
        internal static IkkankaApiClient IkkankaApiClient { get; set; } = new IkkankaApiClient(HatFConfigReader.GetAppSetting("Api:Ikkanka:Uri"));

        /// <summary>HAT-F一貫化APIクライアント</summary>
        internal static HatFApiClient HatFApiClient { get; set; } = new HatFApiClient(HatFConfigReader.GetAppSetting("Api:HatF:Uri"));

        public static ApplicationContext AppContext { get; private set; }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // TODO 利率異常チェック条件を個人ごとではなく部署ごとに設定可能にする
            // 暫定的に条件値を固定し編集不可とする
            Properties.Settings.Default.interestrate_rate_over = 50.ToString();
            Properties.Settings.Default.interestrate_rate_under = string.Empty;
            Properties.Settings.Default.interestrate_suryo_over = 100.ToString();
            Properties.Settings.Default.interestrate_uri_kin_over = 1000000.ToString();
            Properties.Settings.Default.interestrate_uri_tan_zero = true;

            if (Properties.Settings.Default.IsUpgrade == false)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.IsUpgrade = true;
                Properties.Settings.Default.Save();
            }

            Application.ApplicationExit += Application_ApplicationExit;

            // アプリケーション全体に対する集中エラーハンドリング
            Application.ThreadException += Application_ThreadException;
            Thread.GetDomain().UnhandledException += Program_UnhandledException;

            var customInfo = GetExecutionContextInfo();
            ApplicationInsightsHelper.TelemetryClient.TrackTrace("Start Client Application.", SeverityLevel.Information, customInfo);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HatFApiClient.DumpResponse = true;

            AppContext = new ApplicationContext();
            AppContext.MainForm = new Login();
            Application.Run(AppContext);

            ApplicationInsightsHelper.TelemetryClient.TrackTrace("Exit Client Application.", SeverityLevel.Information, customInfo);

            // ログをフラッシュして少し待機後終了
            System.Diagnostics.Debug.WriteLine($"終了前 Application Insights Flush 待ち待機中。");
            ApplicationInsightsHelper.TelemetryClient.Flush();

            // ※デバッグビルドはインメモリチャンネル(即出力)を使用しているので待たずに終了
#if !DEBUG
            // リリースビルドではフラッシュ待ち時間入れる
            Task.Delay(_telemetryFlushingWait).Wait();
#endif

            System.Diagnostics.Debug.WriteLine($"アプリケーション終了。");
        }

        /// <summary>アプリケーション終了ハンドラ</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            IkkankaApiClient.Dispose();
            HatFApiClient.Dispose();
        }

        private static void Program_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void HandleException(Exception ex, [CallerMemberName] string callerMethodName = "")
        {
            // MessageBoxの裏でログ出力とフラッシュしておく
            var task = Task.Run(() =>
            {
                var customInfo = GetExecutionContextInfo();
                ApplicationInsightsHelper.TelemetryClient.TrackException(ex, customInfo);
                ApplicationInsightsHelper.TelemetryClient.TrackTrace("Force Shutdown Client Application", SeverityLevel.Error, customInfo);
                ApplicationInsightsHelper.TelemetryClient.Flush();  // アプリを強制終了する直前なのでフラッシュしておく（ここ以外では省略可）
            });

            // ユーザーに状況通知
            string msg = "エラーが発生しました。エラーが繰り返し発生する場合は管理者に発生日時をご連絡ください。";
            DialogHelper.WarningMessage(null, msg);

            // ※デバッグビルドはインメモリチャンネル(即出力)を使用しているので待たずに終了
#if !DEBUG
            // ログをフラッシュするので少し待機後終了
            System.Diagnostics.Debug.WriteLine($"終了前 Application Insights Flush 待ち待機中。");
            var waitTask = Task.Delay(_telemetryFlushingWait);
            Task.WaitAll(task, waitTask);
#endif

            // 例外が起きた状況を正確に把握して問題を修復して続行するのは不可能に近いので終了する
            // 顧客のデータを破壊するのを最優先で回避する必要がある
            System.Diagnostics.Debug.WriteLine($"アプリケーション終了。");
            Application.Exit();
        }

        /// <summary>
        /// アプリの実行状況情報
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetExecutionContextInfo()
        {
            var executionInfo = new Dictionary<string, string>();
            executionInfo.Add("Application", typeof(Program).Assembly.FullName);
            executionInfo.Add("MachineName", Environment.MachineName);
            executionInfo.Add("UserName", Environment.UserName);

#if DEBUG
            executionInfo.Add("Build", "DEBUG");
#else
            executionInfo.Add("Build", "RELEASE");
#endif

            return executionInfo;
        }
    }
}
