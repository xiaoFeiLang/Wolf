using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Common.JsonHandle
{
    static class JsonToObject
    {

        public static Dictionary<string, object> jsonToDictionary(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                Dictionary<String, Object> dic = jss.Deserialize<Dictionary<string, object>>(jsonData);
                if (dic.Count <= 0)
                {
                    dic.Add("code", 10005);
                    dic.Add("msg", "json解析失败");
                }
                return dic;
            }
            catch
            {
                Dictionary<String, object> dic = new Dictionary<string, object>();
                dic["code"] = 10005;
                dic["msg"] = "json解析失败";
                return dic;
            }
        }
        public static Object[] jsonToArray(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Object[]>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static String dictionaryToJson(Object obj)
        {
            try
            {
                string json = (new JavaScriptSerializer()).Serialize(obj);
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
