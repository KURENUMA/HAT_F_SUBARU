using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HatFClient.Common
{
    internal class UrlUtil
    {
        public static string ToQueryParameterString(NameValueCollection nameValueCollection)
        {
            var sb = new StringBuilder();

            foreach(string key in nameValueCollection.Keys) 
            {
                if (sb.Length > 0) { sb.Append("&"); }

                string encodedKey = HttpUtility.UrlEncode(key);
                sb.Append(encodedKey);

                sb.Append('=');

                string val = nameValueCollection[key];
                string encodedVal = HttpUtility.UrlEncode(val);
                sb.Append(encodedVal);
            }

            return sb.ToString();
        }
    }
}
