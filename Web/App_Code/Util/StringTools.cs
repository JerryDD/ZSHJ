using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Util
{
    /// <summary>
    /// StringTools 的摘要说明

    /// </summary>
    public class StringTools
    {
        public StringTools()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 去除指定字符串两头的空格，如果字符串为空则返回空串
        /// </summary>
        /// <param name="src"></param>
        public static string trim(string src)
        {
            if (src == null)
                return string.Empty;
            return src.Trim();
        }

        /// <summary>
        /// 判断字符串是否为空串或null
        /// </summary>
        /// <param name="src"></param>
        /// <returns>如果字符串为空串或者null则返回true</returns>
        public static bool isBlank(String src)
        {
            if (trim(src).Length == 0)
                return true;
            return false;
        }

        /// <summary>
        /// 字符串转换为整形，转换失败则返回0
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int getInt(string src)
        {
            try
            {
                return Convert.ToInt32(trim(src));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 字符串转换为长整形，转换失败则返回0
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static long getLong(string src)
        {
            try
            {
                return Convert.ToInt64(trim(src));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="src"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetShort(Object src, int len)
        {
            try
            {
                string source = src.ToString();
                source = trim(source);
                if (source.Length <= len)
                {
                    return source;
                }
                string result = source.Substring(0,len)+"...";                
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 将空对象转换为指定字符串
        /// </summary>
        /// <param name="src"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string nvl(Object src, string def)
        {
            if (src == null)
                return def;
            if (isBlank(src.ToString()))
                return def;
            return src.ToString();
        }

        /// <summary>
        /// 将字符串转换为 web格式，如，空格转&nbsp;，换行转br
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string getWebFormat(string src)
        {
            if (src == null)
                return src;
            src = src.Replace("&", "&amp;");
            src = src.Replace("<", "&lt;");
            src = src.Replace(">", "&gt;");
            src = src.Replace(" ", "&nbsp; ");
            src = src.Replace("\r\n", "<br/>");
            src = src.Replace("\r", "<br/>");
            src = src.Replace("\n", "<br/>");
            src = src.Replace("\"", "&quot;");

            return src;
        }

        /// <summary>
        /// 将字符串转换为中的html字符转换为普通文本,如br转换为\r\n
        /// 暂时只实现了空格和换行的转换
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string htmlToText(string src)
        {
            if (src == null)
                return src;
            src = src.Replace("&nbsp;", " ");
            src = src.Replace("&nbsp", " ");
            src = src.Replace("<br/>", "\r\n");
            src = src.Replace("<br>", "\r\n");
            return src;
        }

        
        /// <summary>
        /// 计算字符串占位是否超出指定长度，中文算2位
        /// </summary>
        /// <param name="src"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static bool checkLength(string src, int len)
        {
            if (StringTools.isBlank(src))
            {
                return true;
            }

            int count = 0;
            char[] chars = src.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                //0x0020-0x007Ef（字母数字，标点符号算一位，其它 算2位）
                if (chars[i] >= 0x0020 && chars[i] <= 0x007E)
                    count++;
                else
                    count += 2;
                if (count > len)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string[] getTokens(string src)
        {
            if (StringTools.isBlank(src))
                return new string[0];
            string[] results = src.Split(new String[] { ",", "，", ";", "；", "、" }, StringSplitOptions.RemoveEmptyEntries);
            return results;
        }

        public static DateTime getDate(String src)
        {
            if (StringTools.isBlank(src))
            {
                return DateTime.MinValue;
            }
            else
            {
                try
                {
                    return DateTime.ParseExact(src, "yyyy-MM-dd", null);
                }
                catch (Exception ex)
                {
                    return DateTime.MinValue;
                }
            }

        }

        public static string GetDate(string dt)
        {
            if (dt.Length > 0)
            {
                return dt.Substring(0, 10);
            }
            else
                return "";
        }

        public static string GetTime(string dt)
        {
            if (dt.Length > 0)
            {
                return dt.Substring(11, 8);
            }
            else
                return "";
        }

    }
}
