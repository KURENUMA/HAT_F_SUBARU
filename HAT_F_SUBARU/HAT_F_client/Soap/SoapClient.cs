using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HatFClient.Soap
{
    /// <summary>soap クライアント </summary>
    public class SoapClient
    {
        /// <summary>コンストラクタ</summary>
        public SoapClient()
        {
        }

#if DEBUG

        /// <summary>ダミー応答を使用する</summary>
        public bool UseDummyResponse { get; set; } = true;

        /// <summary>ダミーのwsdlを取得する</summary>
        public Func<string> DummyWsdl { get; set; }

        /// <summary>ダミーのXML応答を取得する</summary>
        public Func<string> DummyResponse { get; set; }

#endif

        /// <summary>エラーの有無</summary>
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        /// <summary>エラーメッセージ</summary>
        public string ErrorMessage { get; private set; }

        /// <summary>リクエスト送信</summary>
        /// <param name="url">リクエスト先</param>
        /// <param name="method">メソッド</param>
        /// <param name="parameters">リクエストパラメータ</param>
        /// <returns>レスポンス</returns>
        public async Task<DataTable> GetAsync(string url, string method, string parameters)
        {
            ErrorMessage = string.Empty;
            var targetNamespace = await GetWsdlAsync(url);
            return string.IsNullOrEmpty(targetNamespace) ? null : await SendRequestAsync(url, method, parameters, targetNamespace);
        }

        /// <summary>wsdlを取得</summary>
        /// <param name="url">URL</param>
        /// <returns>wsdl</returns>
        private async Task<string> GetWsdlAsync(string url)
        {
            var soapRequest = (HttpWebRequest)WebRequest.Create($"{url}?wsdl");
            soapRequest.Method = "GET";

#if DEBUG
            var dummyWsdl = DummyWsdl?.Invoke();
            var dummyResponse = UseDummyResponse && !string.IsNullOrEmpty(dummyWsdl) ? new FakeHttpWebResponse(dummyWsdl) : null;
            using (var response = (HttpWebResponse)(dummyResponse ?? await soapRequest.GetResponseAsync()))
            using (var stream = new StreamReader(response.GetResponseStream()))
#else
            using (var response = (HttpWebResponse)(await soapRequest.GetResponseAsync()))
            using (var stream = new StreamReader(response.GetResponseStream()))
#endif
            {
                var responseXml = stream.ReadToEnd();

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);
                return xmlDocument.DocumentElement.Attributes["targetNamespace"].Value;
            }
        }

        /// <summary>リクエスト送信</summary>
        /// <param name="url"></param>
        /// <param name="method">"ReturnOrderInfo"</param>
        /// <param name="parameters"></param>
        /// <remarks></remarks>
        private async Task<DataTable> SendRequestAsync(string url, object method, object parameters, string targetNamespace)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            builder.AppendLine("<soap:Envelope ");
            builder.AppendLine("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
            builder.AppendLine("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ");
            builder.AppendLine("xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" >");
            builder.AppendLine("<soap:Body>");
            builder.AppendLine($"<{method} xmlns=\"{targetNamespace}\" > ");
            builder.AppendLine(parameters.ToString());
            builder.AppendLine($"</{method}></soap:Body></soap:Envelope>");

#if DEBUG
            var dummyResponseXml = DummyResponse?.Invoke();
            var dummyResponse = UseDummyResponse && !string.IsNullOrEmpty(dummyResponseXml) ? new FakeHttpWebResponse(dummyResponseXml) : null;
            var dummyRequest = dummyResponse is not null ? new FakeHttpWebRequest(dummyResponse) : null;
            // PostメッセージをURLに発信する
            var soapRequest = (HttpWebRequest)(dummyRequest ?? WebRequest.Create(url));
#else
            // PostメッセージをURLに発信する
            var soapRequest = (HttpWebRequest)WebRequest.Create(url);
#endif
            soapRequest.Method = "POST";

            // ヘッダーを追加
            var soapAction = (targetNamespace.EndsWith("/") ? targetNamespace : targetNamespace + "/") + method;
            soapRequest.ContentType = "text/xml;charset=utf-8";
            soapRequest.Headers.Add("SOAPAction", soapAction);

            var requestXml = builder.ToString();
            var bytes = Encoding.UTF8.GetBytes(requestXml);
            using (var requestStream = await soapRequest.GetRequestStreamAsync())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }

            var xmlResult = string.Empty;
            using (var response = (HttpWebResponse)await soapRequest.GetResponseAsync())
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var responseXml = stream.ReadToEnd();

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);
                xmlResult = xmlDocument.DocumentElement.GetElementsByTagName($"{method}Result")[0].OuterXml;
            }

            return ParseResultAsync(xmlResult);
        }

        /// <summary>戻り値（XML）を解析する</summary>
        /// <param name="xml">XML</param>
        /// <returns>解析結果テーブル</returns>
        private DataTable ParseResultAsync(string xml)
        {
            var nt = new NameTable();
            var nsmgr = new XmlNamespaceManager(nt);
            nsmgr.AddNamespace("bk", "urn:sample");

            var context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);

            var firstResponse = true;
            var elementName = string.Empty;
            var names = new List<string>();
            var values = new List<string>();
            var rows = new List<List<string>>();
            using (var reader = new XmlTextReader(xml, XmlNodeType.Element, context))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        elementName = reader.Name;
                    }

                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        // エラーの取得
                        if (elementName == "ErrMsg")
                        {
                            ErrorMessage = reader.Value;
                        }
                        // 名前の取得
                        // NameタグはすべてのTResponseで共通なので、最初のTResponseの場合のみ取得する
                        if (elementName == "Name" && firstResponse)
                        {
                            names.Add(reader.Value);
                        }
                        // 値の取得
                        if (elementName == "Value")
                        {
                            values.Add(reader.Value);
                        }
                    }
                    // 値を空の場合
                    if (reader.IsEmptyElement && elementName == "Value")
                    {
                        values.Add(string.Empty);
                    }
                    // 一行の終る
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "TResponse")
                    {
                        firstResponse = false;
                        rows.Add(values);
                        values = new List<string>();
                    }
                }

                reader.Close();
            }

            var result = new DataTable();
            // columnを追加
            result.Columns.AddRange(names.Select(n => new DataColumn()
            {
                DataType = typeof(string),
                ColumnName = n,
            }).ToArray());

            // 値を追加
            foreach (var row in rows)
            {
                var newRow = result.NewRow();
                for (var i = 0; i < row.Count; i++)
                {
                    newRow[i] = row[i];
                }
                result.Rows.Add(newRow);
            }

            return result;
        }
    }
}