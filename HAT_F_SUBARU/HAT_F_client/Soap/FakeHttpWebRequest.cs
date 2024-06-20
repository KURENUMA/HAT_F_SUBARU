using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HatFClient.Soap
{
#if DEBUG

    /// <summary>SOAP要求オブジェクトのスタブ</summary>
    public class FakeHttpWebRequest : HttpWebRequest
    {
        /// <summary>シリアライズ設定情報</summary>
        private static readonly SerializationInfo SerializationInfo = GetSerializationInfo();

        /// <summary>応答情報</summary>
        private readonly WebResponse _response;

        /// <summary>コンストラクタ</summary>
        /// <param name="response"><see cref="GetResponse"/>および<see cref="GetResponseAsync"/>で返却される応答情報</param>
        public FakeHttpWebRequest(WebResponse response)
#pragma warning disable CS0618
            : base(SerializationInfo, new StreamingContext())
#pragma warning restore CS0618

        {
            _response = response;
        }

        /// <summary>リクエストを送信せずに応答情報を取得する</summary>
        /// <returns>応答情報</returns>
        public override WebResponse GetResponse()
            => _response;

        /// <summary>リクエストを送信せずに応答情報を取得する</summary>
        /// <returns></returns>
        public override Task<WebResponse> GetResponseAsync()
            => Task.FromResult(GetResponse());

        /// <summary>リクエスト情報へのストリームを取得する</summary>
        /// <returns>リクエスト情報へのストリーム</returns>
        public override Stream GetRequestStream()
            => new MemoryStream();

        /// <summary>リクエスト情報へのストリームを取得する</summary>
        /// <returns>リクエスト情報へのストリーム</returns>
        public override Task<Stream> GetRequestStreamAsync()
            => Task.FromResult(GetRequestStream());

        /// <summary>シリアライズ設定情報を取得</summary>
        /// <returns>シリアライズ設定情報</returns>
        private static SerializationInfo GetSerializationInfo()
        {
            // dummy data required for HttpWebRequest() constructor
            var serializationInfo = new SerializationInfo(typeof(HttpWebRequest), new FormatterConverter());
            serializationInfo.AddValue("_HttpRequestHeaders", new WebHeaderCollection(), typeof(WebHeaderCollection));
            serializationInfo.AddValue("_Proxy", null, typeof(IWebProxy));
            serializationInfo.AddValue("_KeepAlive", false);
            serializationInfo.AddValue("_Pipelined", false);
            serializationInfo.AddValue("_AllowAutoRedirect", false);
            serializationInfo.AddValue("_AllowWriteStreamBuffering", false);
            serializationInfo.AddValue("_HttpWriteMode", 0);
            serializationInfo.AddValue("_MaximumAllowedRedirections", 0);
            serializationInfo.AddValue("_AutoRedirects", 0);
            serializationInfo.AddValue("_Timeout", 0);
            serializationInfo.AddValue("_ContentLength", (long)0);
            serializationInfo.AddValue("_MediaType", "");
            serializationInfo.AddValue("_OriginVerb", "GET");
            serializationInfo.AddValue("_ConnectionGroupName", "");
            serializationInfo.AddValue("_Version", new Version(1, 0), typeof(Version));
            serializationInfo.AddValue("_OriginUri", new Uri("https://fake.uri"), typeof(Uri));

            return serializationInfo;
        }
    }

#endif
}