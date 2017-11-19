﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    internal static class QueryTlls
    {
        #region 增加扩展后缀支持
        public static string GetDefaultUrl()
        {
            return AppConfig.GetApp("DefaultUrl", "");
        }

        public static string GetLocalPath()
        {
            string localPath = HttpContext.Current.Request.Url.LocalPath;
            string suffix = AppConfig.GetApp("Taurus.Suffix", "");
            if (suffix != "" && localPath.EndsWith(suffix))
            {
                return localPath.Replace(suffix, "");
            }
            return localPath;
        }
        public static bool IsTaurusSuffix()
        {
            string localPath = HttpContext.Current.Request.Url.LocalPath;
            string suffix = AppConfig.GetApp("Taurus.Suffix", "");
            if (suffix != "" && localPath.EndsWith(suffix))
            {
                return true;
            }
            return localPath.IndexOf('.') == -1;
        }
        public static bool IsAllowCORS()
        {
            return AppConfig.GetAppBool("IsAllowCORS", true);
        }
        #endregion
        /// <summary>
        /// 是否使用子目录部署网站
        /// </summary>
        public static bool IsUseUISite
        {
            get
            {
                string ui = AppConfig.GetApp("UI", string.Empty).ToLower();
                if (ui != string.Empty)
                {
                    ui = ui.Trim('/');
                    string localPath = HttpContext.Current.Request.Url.LocalPath.Trim('/').ToLower();
                    return localPath == ui || localPath.StartsWith(ui + "/");
                }
                return false;
            }
        }

        public static T Query<T>(string key)
        {
            return Query<T>(key, default(T), false);
        }
        public static T Query<T>(string key, T defaultValue, bool filter)
        {
            string value = HttpContext.Current.Request[key];
            return ChangeValueType<T>(value, defaultValue, filter);
        }
        internal static T ChangeValueType<T>(string value, T defaultValue, bool filter)
        {

            if (value == null) { return defaultValue; }
            value = value.Trim();
            object result = null;
            Type t = typeof(T);
            if (t.Name == "String")
            {
                if (filter)
                {
                    result = FilterValue(value);
                }
                else
                {
                    if (value.IndexOf('+') > -1)
                    {
                        string reKey = "[#{@!}#]";
                        string text = value.Replace("+", reKey);//
                        result = HttpContext.Current.Server.UrlDecode(text).Replace(reKey, "+");
                    }
                    else
                    {
                        result = HttpContext.Current.Server.UrlDecode(value);
                    }

                }
            }
            else
            {
                try
                {
                    result = ChangeType(value, t);
                }
                catch
                {
                    return defaultValue;
                }

            }
            return (T)result;
        }

        internal static object ChangeType(object value, Type t)
        {
            return ConvertTool.ChangeType(value, t);
        }
        /// <summary>
        /// 过滤一般的字符串
        /// </summary>
        /// <param name="strFilter"></param>
        /// <returns></returns>
        public static string FilterValue(string strFilter)
        {
            if (strFilter == null)
                return "";
            string returnValue = strFilter;
            string[] filterChar = new string[] { "\'", ",", "(", ")", ";", "\"" };// ">", "<", "=",
            for (int i = 0; i < filterChar.Length; i++)
            {
                returnValue = returnValue.Replace(filterChar[i], "");
            }
            return returnValue.Trim(' ');
        }
    }
}
