using System;
using System.IO;
using System.Net;
using System.Text;

namespace MyClassLibrary
{
    /// <summary>
    /// Http 助手类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="Url"> 请求地址 </param>
        /// <param name="postDataStr"> 请求参数 </param>
        /// <param name="cookie"> 地址相关的 Cookie </param>
        /// <returns> 返回页面数据 </returns>
        public String HttpPost(String Url, String postDataStr, CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);

            request.CookieContainer = cookie;

            Stream myRequestStream = request.GetRequestStream();

            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));

            myStreamWriter.Write(postDataStr);

            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.Cookies = cookie.GetCookies(response.ResponseUri);

            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            String retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();

            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// GET 请求
        /// </summary>
        /// <param name="Url"> 请求地址 </param>
        /// <param name="postDataStr"> 请求参数 </param>
        /// <returns> 返回页面数据 </returns>
        public String HttpGet(String Url, String postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);

            request.Method = "GET";

            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            String retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();

            myResponseStream.Close();

            return retString;
        }
    }
}
