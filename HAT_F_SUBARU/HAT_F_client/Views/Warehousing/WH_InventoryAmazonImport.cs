using ClosedXML.Excel;
using HatFClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using HatFClient.Constants;

namespace HatFClient.Views.Warehousing
{
    public partial class WH_InventoryAmazonImport : Form
    {
        private string _whCode = "";
        private string _whName = "";
        private DateTime _inventoryYearMonth = DateTime.Now;
        private List<StockInventoryAmazon> _dataSource;

        public WH_InventoryAmazonImport() : this(null,null,DateTime.MinValue)
        {
        }

        public WH_InventoryAmazonImport(string whCode, string whName, DateTime inventoryYearMonth)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this);

                _whCode = whCode;
                _whName= whName;
                _inventoryYearMonth = inventoryYearMonth;
            }

        }



        private void WH_InventoryAmazonImport_Load(object sender, EventArgs e)
        {
            txtWarehouse.Text = _whName;
            txtYearMonth.Text = $"{_inventoryYearMonth:yyyy/MM}";
            btnOK.Enabled = false;
            c1gAmazon.AllowEditing = false;
        }

        private void btnOpenAmazonExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel ブック (*.xlsx)|*.xlsx";

                // ダイアログを開いたときのフォルダ
                string documentFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                dialog.InitialDirectory = documentFolder;

                if (DialogHelper.IsPositiveResult(dialog.ShowDialog()))
                {
                    string fileName = dialog.FileName;
                    ReadAmazonInventoryExcel(fileName);
                }
            }
        }

        private void ReadAmazonInventoryExcel(string fileName)
        {
            if (false == FileHelper.CanOpenStream(fileName, FileAccess.Read))
            {
                string message = "ファイルを開くことができません。ファイルを開いているアプリケーションを閉じてください。";
                DialogHelper.WarningMessage(this, message);
                return;
            }

            btnOK.Enabled = false;
            //c1gAmazon.DataSource = null;
            List<StockInventoryAmazon> data = null;

            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (XLWorkbook wb = new(stream))
            {
                foreach (IXLWorksheet ws in wb.Worksheets)
                {
                    if (IsTargetWorkSheet(ws, "HAT在庫"))
                    {
                        data = ReadAmazonData(ws);
                        break;
                    }
                }

            }

            if (data == null)
            {
                string fileNameOnly = Path.GetFileName(fileName);
                string message = $"ファイル「{fileNameOnly}」に「HAT在庫」ワークシートが見つかりませんでした。";
                DialogHelper.WarningMessage(this, message);
                return;
            }

            _dataSource = null;
            rdoFilterAll.Checked = true;
            c1gAmazon.AutoGenerateColumns = false;

            var duplicated = ClientValidation(data);
            c1gAmazon.DataSource = data;

            _dataSource = data;
            lblLoadItemCount.Text = $"読込件数: {data.Count:#,##0}件";

            //var duplicated = ClientValidation(data);
            //if (duplicated.Count > 0)
            //{
            //    //c1gAmazon.DataSource = null;
            //    c1gAmazon.DataSource = data;

            //    string message = $"商品コードが複数の行に重複して存在しています。Excelを修正して再度読み込んでください。{Environment.NewLine}{Environment.NewLine}商品コード: " + string.Join(", ", duplicated);
            //    DialogHelper.WarningMessage(message);
            //    return;
            //}

            var query = data.Where(x => !string.IsNullOrEmpty(x.Description));
            if (query.Any())
            {
                rdoFilterErrorOnly.Checked = true;
                string message = "読み込んだデータにエラーがあります。Excelを修正して再度読み込んでください。";
                DialogHelper.InformationMessage(this, message);
                return;
            }

            string notifyMessage = $"ファイルを読み込みました。{Environment.NewLine}内容を確認して問題がなければ登録してください。";
            DialogHelper.InformationMessage(this, notifyMessage);

            // 保存ボタンを押せるようにする
            btnOK.Enabled = true;
        }

        private List<StockInventoryAmazon> ReadAmazonData(IXLWorksheet ws)
        {
            var data = new List<StockInventoryAmazon>();

            const int colNo = 1;
            const int colProdCode = 2;
            const int colHat = 3;
            const int colAmazon = 4;
            const int colWarehouse = 5;
            const int colInventory = 6;
            const int colDifference = 7;

            bool isFirst = true;
            foreach (var row in ws.RowsUsed())
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }

                var dataRow = new StockInventoryAmazon();

                string cellValue;
                int parsedInt;

                cellValue = GetCellValueString(row.Cell(colNo));
                if (int.TryParse(cellValue, out parsedInt)) { dataRow.No = parsedInt; } else { dataRow.Description = CombineString(dataRow.Description, "No.エラー"); }

                cellValue = row.Cell(colProdCode).Value.ToString();
                dataRow.ProdCode = cellValue;
                if (string.IsNullOrWhiteSpace(cellValue)) 
                {
                    dataRow.Description = CombineString(dataRow.Description, "品番エラー");
                }

                cellValue = GetCellValueString(row.Cell(colHat));
                if (int.TryParse(cellValue, out parsedInt)) { dataRow.StockHat = parsedInt; } else { dataRow.Description = CombineString(dataRow.Description, "HAT在庫エラー"); }

                cellValue = GetCellValueString(row.Cell(colAmazon));
                if (int.TryParse(cellValue, out parsedInt)) { dataRow.StockAmazon = parsedInt; } else { dataRow.Description = CombineString(dataRow.Description, "AMZ在庫エラー"); }

                cellValue = GetCellValueString(row.Cell(colWarehouse));
                if (int.TryParse(cellValue, out parsedInt)) { dataRow.StockWarehouse = parsedInt; } else { dataRow.Description = CombineString(dataRow.Description, "倉庫在庫エラー"); }

                cellValue = GetCellValueString(row.Cell(colInventory));
                if (int.TryParse(cellValue, out parsedInt)) { dataRow.Inventory = parsedInt; } else { dataRow.Description = CombineString(dataRow.Description, "棚卸数量在庫エラー"); }

                cellValue = GetCellValueString(row.Cell(colDifference));
                if (int.TryParse(cellValue, out parsedInt)) { dataRow.Difference = parsedInt; } else { dataRow.Description = CombineString(dataRow.Description, "差異エラー"); }
                
                data.Add(dataRow);
            }

            return data;
        }
        
        private string GetCellValueString(IXLCell cell)
        {
            if (!string.IsNullOrWhiteSpace(cell.FormulaA1))
            {
                // 計算式があるとdeadlockが起きるようなので回避策
                return cell.CachedValue.ToString() ?? "";
            }
            else
            {
                return cell.Value.ToString() ?? "";
            }
        }


        private bool IsTargetWorkSheet(IXLWorksheet ws, string targetName)
        {
            // 半角全角など多少の揺れがあっても同じとみなす
            bool isMatched = string.Equals(Normalize(ws.Name), Normalize(targetName), StringComparison.OrdinalIgnoreCase);
            return isMatched;

            string Normalize(string text)
            {
                return Strings.StrConv(text, VbStrConv.Narrow).Trim();
            }
        }

        //TODO:APIに移動する
        public class StockInventoryAmazon
        {
            public int? No { get; set; }
            public string ProdCode { get; set; }
            public int? StockHat { get; set; }
            public int? StockAmazon { get; set; }
            public int? StockWarehouse { get; set; }
            public int? Inventory { get; set; }
            public int? Difference { get; set; }
            public bool ProdCodeExists { get; set; }
            public string Description { get; set; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 画面を閉じます
            this.DialogResult = DialogResult.Cancel;
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            if (!DialogHelper.SaveItemConfirm(this))
            {
                return;
            }

            var data = _dataSource;

            var query = data.Where(x => !string.IsNullOrEmpty(x.Description));
            if (query.Any())
            {
                rdoFilterErrorOnly.Checked = true;
                string message = "読み込んだデータにエラーがあります。Excelを修正して再度読み込んでください。";
                DialogHelper.InformationMessage(this, message);
                return;
            }

            // 保存
            string url = ApiResources.HatF.Stock.PutInventoryAmazons;
            var param = new Dictionary<string, object>() { { "whCode", _whCode }, { "inventoryYearMonth", _inventoryYearMonth }, { "prodCodeExistsCheckOnly", false } };

            var apiResult = await ApiHelper.UpdateAsync(this, async () =>
            {
                var apiResponse = await Program.HatFApiClient.PutAsync<List<StockInventoryAmazon>>(url, param, data);
                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return;
            }

            data = apiResult.Value;
            c1gAmazon.DataSource = data;
            _dataSource = data;

            query = data.Where(x => !string.IsNullOrEmpty(x.Description));
            if (query.Any())
            {
                rdoFilterErrorOnly.Checked = true;
                string message = "棚卸入力反映時に問題があったため実行されませんでした。Excelを修正して再度読み込んでください。";
                DialogHelper.InformationMessage(this, message);
                return;
            }

            // 画面を閉じます
            this.DialogResult = DialogResult.OK;
        }

        private void btnSavePreCheck_Click(object sender, EventArgs e)
        {
            //List<StockInventoryAmazon> data = _dataSource; // c1gAmazon.DataSource as List<StockInventoryAmazon>;

            //var duplicated = ClientValidation(data);
            //if (duplicated.Count > 0)
            //{
            //    //c1gAmazon.DataSource = null;
            //    c1gAmazon.DataSource = data;

            //    string message = $"商品コードが複数の行に重複して存在しています。Excelを修正して再度読み込んでください。{Environment.NewLine}{Environment.NewLine}商品コード: " + string.Join(", ", duplicated);
            //    DialogHelper.WarningMessage(message);
            //    return;
            //}

            //// 商品コードチェック
            //string url = ApiResources.HatF.PutInventoryAmazons;
            //var param = new Dictionary<string, object>() { { "whCode", _whCode }, { "inventoryYearMonth", _inventoryYearMonth }, { "prodCodeExistsCheckOnly", true } };
            //var apiResult = await ApiHelper.UpdateAsync(this, async () =>
            //{
            //    var apiResponse = await Program.HatFApiClient.PutAsync<List<StockInventoryAmazon>>(url, param, data);
            //    return apiResponse;
            //}, true);

            //if (apiResult.Failed)
            //{
            //    return;
            //}

            //_dataSource = null;
            //rdoFilterAll.Checked = true;
            //data = apiResult.Value;
            //c1gAmazon.DataSource = data;
            //_dataSource = data;

            //btnOK.Enabled = true;
        }

        private List<string> ClientValidation(List<StockInventoryAmazon> data)
        {
            List<string> duplicatedItems = new List<string>();

            foreach(var inv in data)
            {
                var query = data.Where(x => string.Equals(inv.ProdCode, x.ProdCode, StringComparison.OrdinalIgnoreCase));
                if (query.Count() >= 2)
                {
                    inv.Description = CombineString(inv.Description, "商品コード重複");
                    if (!duplicatedItems.Contains(inv.ProdCode)) 
                    {
                        duplicatedItems.Add(inv.ProdCode);
                    }
                }
            }

            return duplicatedItems;
        }

        private string CombineString(string string1, string string2)
        {
            string newString = string.IsNullOrEmpty(string1) ? string2 : $"{string1}, {string2}";
            return newString;
        }


        private void rdoFilterAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked) { return; }
            if (_dataSource == null) { return; }

            c1gAmazon.DataSource = _dataSource;
        }

        private void rdoFilterErrorOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked) { return; }
            if (_dataSource == null) { return; }

            var data = _dataSource.Where(x => !string.IsNullOrEmpty(x.Description)).ToList();
            c1gAmazon.DataSource = data;
        }
    }
}
