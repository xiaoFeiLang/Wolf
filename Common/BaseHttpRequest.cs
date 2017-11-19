using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Common
{
    class BaseHttpRequest
    {
        private String url;
        private Dictionary<String, Object> body;
        private Action<Stream> successAction;
        private Action<String> errorAction;
        private HttpWebRequest request;
        private HttpWebResponse response;
        public Dictionary<String, String> cookieDic;

        public BaseHttpRequest Post(String url)
        {
            this.url = url;
            this.request = (HttpWebRequest)HttpWebRequest.Create(this.url);
            this.request.Method = "POST";
            return this;
        }
        public BaseHttpRequest Get(String url)
        {
            this.url = url;
            this.request = (HttpWebRequest)HttpWebRequest.Create(this.url);
            this.request.Method = "GET";
            return this;
        }
        public BaseHttpRequest Form(Object body)
        {
            this.body = this.ToMap(body);
            StringBuilder parameter = new StringBuilder();
            foreach (KeyValuePair<String, Object> pair in this.body)
            {
                parameter.AppendFormat("{0}={1}&", pair.Key, pair.Value);
            }
            parameter.Remove(parameter.Length - 1, 1);
            byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(parameter.ToString());
            using (Stream reqStream = this.request.GetRequestStream())
            {
                reqStream.Write(bt, 0, bt.Length);
            }
            return this;
        }
        public BaseHttpRequest FormDic(Dictionary<String, Object> body)
        {
            this.body = body;
            StringBuilder parameter = new StringBuilder();
            foreach (KeyValuePair<String, Object> pair in this.body)
            {
                if (pair.Value.GetType().ToString().Equals("System.Collections.Generic.List`1[System.Collections.Generic.Dictionary`2[System.String,System.Object]]"))
                {
                    List<Dictionary<String, object>> list = pair.Value as List<Dictionary<String, object>>;
                    foreach (Dictionary<String, object> d in list)
                        foreach (KeyValuePair<String, Object> p in d)
                            parameter.AppendFormat("{0}={1}&", p.Key, p.Value);
                }
                else
                    parameter.AppendFormat("{0}={1}&", pair.Key, pair.Value);
            }
            parameter.Remove(parameter.Length - 1, 1);
            byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(parameter.ToString());
            using (Stream reqStream = this.request.GetRequestStream())
            {
                reqStream.Write(bt, 0, bt.Length);
            }
            return this;
        }
        public BaseHttpRequest SetHeader(String referer, String origin, String accept, String contentType, String sign)
        {
            if (!String.IsNullOrEmpty(accept))
                request.Accept = accept;// "application/json, text/javascript, */*; q=0.01";
            if (!String.IsNullOrEmpty(contentType))
                request.ContentType = contentType;// "application/x-www-form-urlencoded; charset=UTF-8";
            if (!String.IsNullOrEmpty(referer))
                request.Referer = referer;
            if (!String.IsNullOrEmpty(origin))
                request.Headers.Add("Origin", origin);
            if (!String.IsNullOrEmpty(sign))
                request.Headers.Add("X-Sign", sign);

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            return this;
        }
        public BaseHttpRequest SetHeader(String referer, String origin, String accept, String contentType)
        {
            if (!String.IsNullOrEmpty(accept))
                request.Accept = accept;// "application/json, text/javascript, */*; q=0.01";
            if (!String.IsNullOrEmpty(contentType))
                request.ContentType = contentType;// "application/x-www-form-urlencoded; charset=UTF-8";
            if (!String.IsNullOrEmpty(referer))
                request.Referer = referer;
            if (!String.IsNullOrEmpty(origin))
                request.Headers.Add("Origin", origin);

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            return this;
        }
        public BaseHttpRequest setCookies(Dictionary<String, String> cookieDic)
        {
            this.cookieDic = cookieDic;
            CookieContainer cookie = new CookieContainer();
            //Uri uri = new Uri("http://www.ji7.com/");
            Uri uri = new Uri("http://m.ji7.com/cltx");
            foreach (KeyValuePair<string, string> kv in this.cookieDic)
            {
                cookie.SetCookies(uri, String.Format("{0}={1}", kv.Key, kv.Value));
            }
            this.request.CookieContainer = cookie;
            return this;
        }
        public BaseHttpRequest OnSuccess(Action<Stream> jsonAction)
        {
            this.successAction = jsonAction;
            return this;
        }
        public BaseHttpRequest OnFail(Action<String> errorAction)
        {
            this.errorAction = errorAction;
            return this;
        }

        public void Go()
        {
            try
            {
                this.response = (HttpWebResponse)this.request.GetResponse();

                foreach (Cookie c in this.response.Cookies)
                {
                    this.cookieDic[c.Name] = c.Value;
                }
                Stream stream = this.response.GetResponseStream();   //获取响应的字符串流  
                this.successAction(stream);
            }
            catch (Exception ex)
            {
                this.errorAction(ex.Message);
            }
        }

        public void close()
        {
            this.response.Close();
            this.request.Abort();
        }

        /// <summary>  
        ///   
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        private Dictionary<String, Object> ToMap(Object o)
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();
            Type t = o.GetType();
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }
            return map;
        }
    }
}
