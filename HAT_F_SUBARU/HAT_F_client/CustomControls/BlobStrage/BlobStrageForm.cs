using DocumentFormat.OpenXml.Office2010.ExcelAc;
using HAT_F_api.CustomModels;
using HAT_F_client.Views.BlobStorage;
using HatFClient.Common;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.CustomControls.BlobStrage
{
    public partial class BlobStrageForm : UserControl
    {
        public string StrageId { get; set; }

        private bool _canUpload = true;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUpload
        {
            get => _canUpload;
            set
            {
                _canUpload = value;
                lblDiscription.Visible = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDownload { get; set; } = true;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDelete { get; set; } = true;

        BlobStrageRepo blobRepo = null;

        public BlobStrageForm()
        {
            InitializeComponent();

            setGridView();

            blobRepo = BlobStrageRepo.GetInstance();
            //BlobStorageForm_Load();
            dgvFiles.DataSource = blobRepo.BlobFileInfos;
        }

        private void setGridView()
        {
            dgvFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // CHECKBOX
            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
            col1.Name = "Checked";
            col1.DataPropertyName = "Checked";
            col1.HeaderText = "";
            col1.Width = 50;
            col1.FillWeight = 20;
            dgvFiles.Columns.Add(col1);

            // NAME
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Name";
            col2.HeaderText = "ファイル名";
            col2.ReadOnly = true;
            dgvFiles.Columns.Add(col2);

            // ContentLength
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "ContentLength";
            col3.HeaderText = "ファイルサイズ";
            col3.ReadOnly = true;
            dgvFiles.Columns.Add(col3);

            // CreatedOn
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "CreatedOn";
            col4.HeaderText = "作成日";
            col4.ReadOnly = true;
            dgvFiles.Columns.Add(col4);

            // LastModified
            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "LastModified";
            col5.HeaderText = "最終更新日";
            col5.ReadOnly = true;
            dgvFiles.Columns.Add(col5);

        }

        private void validateControls()
        {
            var btnEnabled = blobRepo.BlobFileInfos.Any(b => b.Checked);
            this.btnDelete.Enabled = CanDelete && btnEnabled;
            this.btnDownload.Enabled = CanDownload && btnEnabled;
        }

        private void dgvFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (CanUpload && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void dgvFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in droppedFiles)
                {
                    var fileInfo = new FileInfo(file);
                    var blobFileInfo = blobRepo.Find(fileInfo.Name);
                    var message = "";
                    var title = "";
                    if (blobFileInfo != null)
                    {
                        title = "ファイル更新";
                        message = $"{fileInfo.Name}は既に存在します。上書きしてよろしいですか？";
                    }
                    else
                    {
                        title = "ファイル登録";
                        message = $"{fileInfo.Name}をアップロードしてよろしいですか？";
                    }
                    var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        await blobRepo.Upload(file);
                    }
                }
            }
            e.Effect = DragDropEffects.None;
            Debug.WriteLine("DONE");
        }
        private void dgvFiles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine($"CellValueChanged:{dgvFiles.IsCurrentCellDirty}");

        }

        private void dgvFiles_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"CurrentCellDirtyStateChanged:{dgvFiles.IsCurrentCellDirty}");
            if (dgvFiles.IsCurrentCellDirty)
            {
                dgvFiles.CommitEdit(DataGridViewDataErrorContexts.Commit);
                validateControls();
            }

        }

        public async void Init(string StrageId)
        {
            await blobRepo.Init(StrageId);

            validateControls();
        }

        public async Task<List<string>> DownloadAllAsync()
        {
            var resultList = new List<string>();
            var folderPath = BlobStrageUtil.GetTempOutputPath();
            var downloadList = blobRepo.BlobFileInfos.ToList();
            for (var i = 0; i < downloadList.Count(); i++)
            {
                var blobFileInfo = downloadList.ElementAt(i);
                await blobRepo.Download(folderPath, blobFileInfo);
                resultList.Add(Path.Combine(folderPath, blobFileInfo.Name));
            }
            return resultList;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("選択したファイルをクラウドから削除してもよろしいですか？", "ファイル削除", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var deleteList = blobRepo.BlobFileInfos.Where(b => b.Checked).ToList();
                var progressDialog = new FormProgress();
                progressDialog.SetProgress("", 0);
                progressDialog.Show();
                double unit = 100 / deleteList.Count();
                for (var i = 0; i < deleteList.Count(); i++)
                {
                    int progress = (int)Math.Round(i * unit);
                    var blobFileInfo = deleteList.ElementAt(i);
                    progressDialog.SetProgress(blobFileInfo.Name, progress);

                    await blobRepo.Delete(blobFileInfo);

                    dgvFiles.Refresh();
                }
                progressDialog.Close();
                progressDialog.Dispose();

                dgvFiles.Refresh();
                MessageBox.Show("削除しました。", "ファイル削除", MessageBoxButtons.OK);
            }

        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // ダイアログの説明テキストを設定（オプション）
                folderBrowserDialog.Description = "ダウンロード先のフォルダを選択してください。";

                // ダイアログを表示
                DialogResult result = folderBrowserDialog.ShowDialog();

                // ユーザーがフォルダを選択した場合
                if (DialogHelper.IsPositiveResult(DialogResult.OK) && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // 選択されたパスを使用して何かを行う
                    var downloadList = blobRepo.BlobFileInfos.Where(b => b.Checked).ToList();
                    var progressDialog = new FormProgress();
                    progressDialog.SetProgress("", 0);
                    progressDialog.Show();
                    double unit = downloadList.Count() > 0 ? 100 / downloadList.Count() : 100;
                    for (var i = 0; i < downloadList.Count(); i++)
                    {
                        int progress = (int)Math.Round(i * unit);
                        var blobFileInfo = downloadList.ElementAt(i);
                        progressDialog.SetProgress(blobFileInfo.Name, progress);
                        await blobRepo.Download(folderBrowserDialog.SelectedPath, blobFileInfo);
                        blobFileInfo.Checked = false;


                        dgvFiles.Refresh();
                    }
                    progressDialog.Close();
                    progressDialog.Dispose();

                    dgvFiles.Refresh();
                    MessageBox.Show("ダウンロードしました。", "ダウンロード", MessageBoxButtons.OK);
                }
            }
        }
    }
}
