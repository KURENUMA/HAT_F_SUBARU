using HatFClient.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace HatFClient.Common
{
    internal class ExcelReportUtil
    {
        private static readonly string TemplatePath = HatFConfigReader.GetAppSetting("Reporting:ExcelTemplatePath");
        private static readonly string TempExcelOutputPath = HatFConfigReader.GetAppSetting("Reporting:ExcelOutputTempPath");
        private static readonly string ExcelOutputPath = HatFConfigReader.GetAppSetting("Reporting:ExcelOutputPath");

        /// <summary>
        /// 指定ファイル名にテンプレートファイルのパスを付与してフルパスにします。
        /// </summary>
        public static string AddTemplatePathToFileName(string templateFileName)
        {
            string fileName = Path.Combine(TemplatePath, templateFileName);
            return fileName;
        }

        /// <summary>
        /// Excel帳票用の一時ファイル名を作成します。
        /// </summary>
        /// <param name="baseFileName"></param>
        /// <returns></returns>
        public static string ToExcelReportTempFileName(string baseFileName)
        {
            string fileName = Environment.ExpandEnvironmentVariables(TempExcelOutputPath);

            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }

            fileName = Path.Combine(fileName, baseFileName);
            return fileName;
        }

        /// <summary>
        /// 永続的に保存されるExcel帳票用のファイル名を作成します。
        /// </summary>
        /// <param name="baseFileName">Excelファイル名</param>
        /// <returns></returns>
        public static string ToExcelReportFileName(string baseFileName)
        {
            return ToExcelReportFileName(baseFileName, "");
        }

        /// <summary>
        /// 永続的に保存されるExcel帳票用のファイル名を作成します。
        /// </summary>
        /// <param name="baseFileName">Excelファイル名</param>
        /// <param name="category">分類名(≒サブディレクトリ)</param>
        /// <returns></returns>
        public static string ToExcelReportFileName(string baseFileName, string category)
        {
            string fileName = Environment.ExpandEnvironmentVariables(ExcelOutputPath);
            if (!string.IsNullOrEmpty(category)) 
            {
                fileName = Path.Combine(fileName, category);
            }

            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }

            fileName = Path.Combine(fileName, baseFileName);
            return fileName;
        }

        /// <summary>
        /// 注文書Excelファイルの「名前を付けて保存」ダイアログを生成します。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks>ダイアログのインスタンスはDisposeしてください。</remarks>
        public static SaveFileDialog CreateOrderFormSaveFileDialog(string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // ダイアログを開いたときに提示されるファイル名
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "Excel ブック (*.xlsx)|*.xlsx";

            // ダイアログを開いたときのフォルダ
            string documentFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.InitialDirectory = documentFolder;

            saveFileDialog.AddExtension = true;
            saveFileDialog.AutoUpgradeEnabled = true;
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Title = "名前を付けて保存";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.RestoreDirectory = true;

            return saveFileDialog;
        }
    }
}
