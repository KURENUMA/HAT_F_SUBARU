using HatFClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using C1.Win.C1FlexGrid;
using HatFClient.Repository;

namespace HatFClient.Views.ImportDeliveryData
{
    public partial class ImportData : Form
    {
        public ImportData()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
            }

            //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            Graphics g = e.Graphics;

            // 黒色で太さ2のペンを作成
            using (Pen pen = new Pen(Color.Black, 2))
            {
                // 枠線の描画
                g.DrawRectangle(pen, 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            }
        }


        #region <ボタン>
        private void btnCOPY_CLIPBOARD_IMG_Click(object sender, EventArgs e)
        {
            Image img = Clipboard.GetImage();
            if (img != null)
            {
                pictureBox1.Image = img;
            }
            else
            {
                DialogHelper.WarningMessage(this, "クリップボードに画像がありません。");
            }
        }

        private /*async*/ void btnIMG_EXCEL_ClickAsync(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                /*
                var base64String = ImageToBase64(pictureBox1.Image, ImageFormat.Png);

                var result = await Task.Run(() => PdfToJsonString(base64String));


                Console.WriteLine(result);
                var result = await Task.Run(() => PdfToJsonString(base64);

                var wb = new XLWorkbook();
                // Add a worksheet and set some data
                var ws = wb.Worksheets.Add("Sheet1");



                ws.Cell("A1").Value = "Hello";
                ws.Cell("B1").Value = "World";


                // Save the workbook to a MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.Position = 0;

                    // Prompt the user to save the file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveFileDialog.FileName = "example.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Save the MemoryStream to the selected file
                        using (FileStream file = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            ms.WriteTo(file);
                        }
                    }
                }
                */
            }
        }

        private void btnCOPY_CRIPBADRD_GRID_Click(object sender, EventArgs e)
        {
            // クリップボードのテキストを取得します
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Text))
            {
                string str1, str2;
                // クリップボードからデータを取得します
                str1 = (string)data.GetData(DataFormats.Text);

                // クリップボードから最後の行のフィードコードを削除します
                str2 = str1.Remove(str1.Length - 1, 1);

                // 範囲を選択してデータを貼り付けます
                c1FlexGrid1.Select(c1FlexGrid1.Row, c1FlexGrid1.Col, c1FlexGrid1.Rows.Count - 1, c1FlexGrid1.Cols.Count - 1, true);
                c1FlexGrid1.Clip = str2;
                c1FlexGrid1.Select(c1FlexGrid1.Row, c1FlexGrid1.Col);
            }
        }
        #endregion

        //private async Task<string> AnalyzeTextWithChatGPT(string text)
        //{
        //    var result = await Program.HatFApiClient.GetAsync<string>(
        //        ApiResources.HatF.Client.GptPdf, new Dictionary<string, object>
        //        {
        //            {"pdfstring", text},
        //        });
        //    return result.Data.ToString();
        //}

        public string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // 画像をストリームに保存
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // バイト配列をBase64文字列に変換
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }


        private const string _uri = "https://openai-dis-div-east-us.openai.azure.com/";
        private const string _key = "63ef1827a3f3434eb619c991e321b65d";
        private const string _deploymentname = "deployments-gpt4o-dis-div";

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
        private static OpenAIClient NewOpenAiClient()
        {
            OpenAIClient client = new(new Uri(_uri), new AzureKeyCredential(_key));
            return client;
        }
        public async Task<string> PdfToJsonString(string pdfstring)
        {
            OpenAIClient client = NewOpenAiClient();
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = _deploymentname,
                Messages =
                    {
                        new ChatRequestSystemMessage("You are an AI assistant that helps people find information."),
                        new ChatRequestUserMessage( $"Here is an image in base64 format: {pdfstring}")
                    }
            };
            client.GetChatCompletionsStreamingAsync(chatCompletionsOptions).Wait();
            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
            ChatResponseMessage responseMessage = response.Value.Choices[0].Message;

            var responseJson = responseMessage.Content;
            return responseJson;
        }
        */
    }
}