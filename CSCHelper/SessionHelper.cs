using System;
using System.Web;

namespace MyClassLibrary
{
    public class SessionHelper
    {
        /// <summary>
        /// 根据Session名获取Session对象
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns>Session对象</returns>
        public static Object Get(String name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="name">session 名</param>
        /// <param name="val">session 值</param>
        public static void Set(String name, Object val)
        {
            HttpContext.Current.Session.Remove(name);

            HttpContext.Current.Session.Add(name, val);
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void Add(String strSessionName, String strValue, Int32 iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;

            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValues">Session值数组</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void Add(String strSessionName, String[] strValues, Int32 iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValues;

            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static String GetValue(String strSessionName)
        {
            return HttpContext.Current.Session[strSessionName] == null ? null : HttpContext.Current.Session[strSessionName].ToString();
        }

        /// <summary>
        /// 读取某个Session对象值数组
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值数组</returns>
        public static String[] GetValues(String strSessionName)
        {
            return HttpContext.Current.Session[strSessionName] == null ? null : (String[])HttpContext.Current.Session[strSessionName];
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Delete(String strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
    }
}
