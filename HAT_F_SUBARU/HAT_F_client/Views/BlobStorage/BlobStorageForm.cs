using HAT_F_api.CustomModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HatFClient.Repository;
using HatFClient.Common;

namespace HAT_F_client.Views.BlobStorage
{
    public partial class BlobStorageForm : Form
    {
        public String DenNo { get; set; }
        BlobRepo blobRepo = null;

        public BlobStorageForm()
        {
            InitializeComponent();

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // CHECKBOX
            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
            col1.Name = "Checked";
            col1.DataPropertyName = "Checked";
            col1.HeaderText = "";
            col1.Width = 50;
            dataGridView1.Columns.Add(col1);

            // NAME
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Name";
            col2.HeaderText = "ファイル名";
            col2.ReadOnly = true;
            dataGridView1.Columns.Add(col2);

            // NAME
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "ContentLength";
            col3.HeaderText = "ファイルサイズ";
            col3.ReadOnly = true;
            dataGridView1.Columns.Add(col3);
            // NAME
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "CreatedOn";
            col4.HeaderText = "作成日";
            col4.ReadOnly = true;
            dataGridView1.Columns.Add(col4);

            // NAME
            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "LastModified";
            col5.HeaderText = "最終更新日";
            col5.ReadOnly = true;
            dataGridView1.Columns.Add(col5);


            blobRepo = BlobRepo.GetInstance();
            dataGridView1.DataSource = blobRepo.BlobFileInfos;
        }

        private void validateControls() {
            var btnEnabled = blobRepo.BlobFileInfos.Any(b => b.Checked);
            this.btnDelete.Enabled = btnEnabled;
            this.btnDownload.Enabled = btnEnabled;
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            } else
            {
                e.Effect= DragDropEffects.None;
            }
        }

        private async void dataGridView1_DragDrop(object sender, DragEventArgs e)
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
                    if (blobFileInfo != null) {
                        title = "ファイル更新";
                        message = $"{fileInfo.Name}は既に存在します。上書きしてよろしいですか？";
                    } else {
                        title = "ファイル登録";
                        message = $"{fileInfo.Name}をアップロードしてよろしいですか？";
                    }
                    var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) {
                        await blobRepo.Upload(file);
                    }
                }
            }
            e.Effect= DragDropEffects.None;
            Debug.WriteLine("DONE");
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine($"CellValueChanged:{dataGridView1.IsCurrentCellDirty}");

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"CurrentCellDirtyStateChanged:{dataGridView1.IsCurrentCellDirty}");
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                validateControls();
            }

        }

        private async void BlobStorageForm_Load(object sender, EventArgs e)
        {
            await blobRepo.Init(this.DenNo);

            validateControls();
        }

        private async void btnDelete_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("選択したファイルをクラウドから削除してもよろしいですか？", "ファイル削除", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                var deleteList = blobRepo.BlobFileInfos.Where(b => b.Checked).ToList();

                using (var progressDialog = new FormProgress())
                {
                    progressDialog.SetProgress("", 0);
                    progressDialog.Show();
                    double unit = 100 / deleteList.Count();
                    for (var i = 0; i < deleteList.Count(); i++)
                    {
                        int progress = (int)Math.Round(i * unit);
                        var blobFileInfo = deleteList.ElementAt(i);
                        progressDialog.SetProgress(blobFileInfo.Name, progress);

                        await blobRepo.Delete(blobFileInfo);

                        dataGridView1.Refresh();
                    }
                    progressDialog.Close();
                    progressDialog.Dispose();
                }

                dataGridView1.Refresh();
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
                if (DialogHelper.IsPositiveResult(result) && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // 選択されたパスを使用して何かを行う
                    var downloadList = blobRepo.BlobFileInfos.Where(b => b.Checked).ToList();

                    using (var progressDialog = new FormProgress())
                    {
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


                            dataGridView1.Refresh();
                        }
                        progressDialog.Close();
                        progressDialog.Dispose();
                    }

                    dataGridView1.Refresh();
                    MessageBox.Show("ダウンロードしました。", "ダウンロード", MessageBoxButtons.OK); 
                }
            }
        }
    }
}
