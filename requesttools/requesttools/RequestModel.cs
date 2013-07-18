using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace requesttools
{
    public class RequestModel
    {
        /// <summary>
        /// Url（无参数）
        /// </summary>
        public string Url { get; set; }

        public string InputCharset { get; set; }
        /// <summary>
        /// 参数(a=1&b=2&...)
        /// </summary>
        public List<KeyValuePair<string, string>> Parameters
        { get; set; }

        /// <summary>
        /// 添加新参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="addEmpty">是否添加空值(默认否)</param>
        public void AddNewParameter(string key, string value, bool addEmpty = false)
        {
            if (string.IsNullOrEmpty(key)) return;
            key = key.Trim();


            if (Parameters == null)
            {
                Parameters = new List<KeyValuePair<string, string>>();
            }
            if (!addEmpty && string.IsNullOrEmpty(value)) return;

            Parameters.Add(new KeyValuePair<string, string>(key, value));
        }
    }
}
