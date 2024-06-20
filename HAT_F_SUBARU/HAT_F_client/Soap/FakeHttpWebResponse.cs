using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HatFClient.Soap
{
#if DEBUG

    /// <summary>SOAP応答オブジェクトのスタブ</summary>
    public class FakeHttpWebResponse : HttpWebResponse
    {
        /// <summary>シリアライズ設定情報</summary>
        private static readonly SerializationInfo SerializationInfo = GetSerializationInfo();

        /// <summary>応答情報としてのストリーム</summary>
        private readonly Stream _responseStream;

        /// <summary>コンストラクタ</summary>
        /// <param name="xml">応答情報XML</param>
        public FakeHttpWebResponse(string xml)
            : this(SerializationInfo, new StreamingContext())
        {
            _responseStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="serializationInfo">シリアライズ設定情報</param>
        /// <param name="streamingContext">コンテキスト</param>
        protected FakeHttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
#pragma warning disable CS0618
            : base(serializationInfo, streamingContext)
#pragma warning restore CS0618
        {
        }

        /// <summary>HTTPステータスコード</summary>
        public override HttpStatusCode StatusCode { get; }

        /// <summary>応答情報のストリームを取得</summary>
        /// <returns>応答情報のストリーム</returns>
        public override Stream GetResponseStream()
            => _responseStream;

        /// <summary>シリアライズ設定情報を取得</summary>
        /// <returns>シリアライズ設定情報</returns>
        private static SerializationInfo GetSerializationInfo()
        {
            // dummy data required for HttpWebResponse() constructor
            var serializationInfo = new SerializationInfo(typeof(HttpWebResponse), new FormatterConverter());
            serializationInfo.AddValue("m_HttpResponseHeaders", new WebHeaderCollection(), typeof(WebHeaderCollection));
            serializationInfo.AddValue("m_Uri", new Uri("https://fake.uri"), typeof(Uri));
            serializationInfo.AddValue("m_Certificate", null, typeof(X509Certificate));
            serializationInfo.AddValue("m_Version", new Version(), typeof(Version));
            serializationInfo.AddValue("m_StatusCode", (int)HttpStatusCode.HttpVersionNotSupported);
            serializationInfo.AddValue("m_ContentLength", (long)0);
            serializationInfo.AddValue("m_Verb", "GET");
            serializationInfo.AddValue("m_StatusDescription", "");
            serializationInfo.AddValue("m_MediaType", "");
            return serializationInfo;
        }

        /// <summary>解放</summary>
        /// <param name="disposing">Dispose()による解放かどうか</param>
        protected override void Dispose(bool disposing)
        {
            _responseStream.Dispose();
            base.Dispose(disposing);
        }
    }

#endif
}