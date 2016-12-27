using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Util;
using System.Text;
using System.Net.Mail;


/// <summary>
/// 系统全局辅助类
/// </summary>
public class GlobalHelper
{
    public GlobalHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 限制提交间隔时间
    /// </summary>
    /// <param name="Session"></param>
    /// <param name="timeSecond"></param>
    /// <param name="lastTimeKey"></param>
    /// <returns>大于间隔时间返回:True</returns>
    public static bool ValidSubmit(System.Web.SessionState.HttpSessionState Session, int timeSecond, string lastTimeKey)
    {
        lock (Session)
        {
            DateTime now = DateTime.Now;
            if (Session[lastTimeKey] != null)
            {
                DateTime lastTime = Convert.ToDateTime(Session[lastTimeKey]);
                //连续提交时间            
                if (lastTime.AddSeconds(timeSecond).CompareTo(now) >= 0)
                {
                    return false;
                }
            }
            Session[lastTimeKey] = now;
            return true;
        }
    }

    public static void ErrorPage(System.Web.UI.Page page, string msg)
    {
        string url = "~/MessagePage.aspx?msg=" + HttpUtility.UrlEncode(msg, Encoding.GetEncoding("utf-8"));
        page.Response.Redirect(url);
    }

    public static void ErrorPage(System.Web.UI.Page page, string msg,bool endResponse)
    {
        string url = "~/MessagePage.aspx?msg=" + HttpUtility.UrlEncode(msg, Encoding.GetEncoding("utf-8"));
        page.Response.Redirect(url, endResponse);
    }

    /// <summary>
    /// 获取当前web应用的根路径，去掉了路径中最后一个"/"
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    public static string GetAppPath(HttpRequest Request)
    {
        String path = Request.ApplicationPath;
        if (StringTools.isBlank(path))
            return "";
        if (path.EndsWith("/"))
        {
            path = path.Remove(path.Length - 1);
        }
        return path;
    }

    
    /// <summary>
    /// 获取字符串的web表现形式
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string getWebFormat(Object str)
    {
        if (str == null)
            return null;
        return StringTools.getWebFormat(Convert.ToString(str));
    }

    /// <summary>
    /// 以字符串的方式判断指定的值是否与指定容器内某字段的值相同
    /// </summary>
    /// <param name="container">容器</param>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static bool Equal(Object container, string field, string value)
    {
        try
        {
            Object source = DataBinder.Eval(container, field);
            if (value.Equals(Convert.ToString(source)))
                return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// 记录购买使用等商业逻辑日志
    /// </summary>
    /// <param name="log"></param>
    public static void BussinessLog(string log)
    {
        LogHelper.log4netToName(log, "BussinessLog");
    }

    /// <summary>
    /// 记录购买使用等商业逻辑日志
    /// </summary>
    /// <param name="log"></param>
    public static void BussinessErrorLog(string log)
    {
        LogHelper.log4netToName(log, "BussinessErrorLog");
    }
}
