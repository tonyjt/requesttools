using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace requesttools
{
    public class HttpRequestManager
    {
        private static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(HttpRequestManager));

        public static string SendRequest(string url, List<KeyValuePair<string,string>> parameters, bool isPost = true, int timeOut = 0, string encoding = "gb2312")
        {
            RequestModel model = new RequestModel
            {
                Url= url,
                Parameters = parameters
            };
            return SendRequest(model, isPost, timeOut, encoding);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="isPost">是否Post</param>
        /// <param name="timeOut">过期时间（为0则不设置过期时间）</param>
        /// <returns></returns>
        public static string SendRequest(RequestModel urlModel, bool isPost = true, int timeOut = 0, string encoding = "gb2312")
        {
            GZipWebClient webClient = new GZipWebClient(timeOut);

            try
            {
                if (!isPost)
                {
                    string address = ConvertUrlSimpleModelToGetRequest(urlModel);


                    StreamReader Reader = new StreamReader(webClient.OpenRead(address), Encoding.GetEncoding(encoding));

                    string returnResponse = Reader.ReadToEnd();

                    return returnResponse;
                }
                else
                {
                    string para = ConvertKeyValuePairListToQueryString(urlModel.Parameters, true);

                    return webClient.UploadString(urlModel.Url, para);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Request Error:{0}, exception:{1}",urlModel.Url,ex.Message));
                return string.Empty;
            }
        }

        /// <summary>
        /// UrlSimpleModel=>Request Address(Get)
        /// </summary>
        /// <param name="urlModel"></param>
        /// <returns></returns>
        private static string ConvertUrlSimpleModelToGetRequest(RequestModel urlModel, bool urlEncode = true)
        {
            if (urlModel.Parameters != null && urlModel.Parameters.Count > 0)
                return urlModel.Url + "?" + ConvertKeyValuePairListToQueryString(urlModel.Parameters, urlEncode);
            else return urlModel.Url;
         
        }

        /// 从KeyValuePairList=>QueryString
        /// </summary>
        /// <returns></returns>
        private static string ConvertKeyValuePairListToQueryString(List<KeyValuePair<string, string>> parameters, bool urlEncode)
        {
            StringBuilder sbParas = new StringBuilder();

            foreach (KeyValuePair<string, string> para in parameters)
            {
                sbParas.Append(para.Key + "=" + (urlEncode ? HttpUtility.UrlEncode(para.Value, Encoding.UTF8) : para.Value) + "&");
                //sbParas.Append(para.Key + "=" + para.Value + "&");
            }

            return sbParas.ToString().Substring(0, sbParas.Length > 1 ? sbParas.Length - 1 : 0);
        }

        public static string GetCurrentRootUrl()
        {
            string virtualPath = HttpContext.Current.Request.ApplicationPath == "/" ? "/" : HttpContext.Current.Request.ApplicationPath + "/";

            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + virtualPath;
        }

        public static string GenerateRequest(string url, List<KeyValuePair<string, string>> parameters, bool urlEncode = true)
        {
            RequestModel urlModel = new RequestModel
            {
                Url = url,
                Parameters = parameters
            };

            return ConvertUrlSimpleModelToGetRequest(urlModel, urlEncode);
        }

    }
}
