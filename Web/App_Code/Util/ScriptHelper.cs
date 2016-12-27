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
    /// ScriptHelper 的摘要说明
    /// </summary>
    public class ScriptHelper
    {
        public ScriptHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 再指定页面弹出消息
        /// </summary>
        /// <param name="page">web窗体实体</param>
        /// <param name="msg">要alert的消息</param>
        public static void addScript(System.Web.UI.Page page, string addscript)
        {
            string script = "<script language='javascript'>" + addscript + "</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", script);
        }


        /// <summary>
        /// 再指定页面弹出消息
        /// </summary>
        /// <param name="page">web窗体实体</param>
        /// <param name="msg">要alert的消息</param>
        public static void addMsg(System.Web.UI.Page page, string msg)
        {
            string script = "<script language='javascript'>alert('" + msg + "')</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", script);
        }

        /// <summary>
        /// 弹出消息后跳转到指定页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void jumpMsg(System.Web.UI.Page page, string msg, string url)
        {
            url = StringTools.trim(url).Replace("\"", "\\\"");
            url = url.Replace("\\", "\\\\");
            msg = msg.Replace("\n", "\\n");
            string script = "<script language='javascript'>alert('" + msg + "');"
                          + " window.location=\"" + url + "\";"
                          + "</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", script);
        }

        /// <summary>
        /// 确认消息后跳转
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void confirmJumpMsg(System.Web.UI.Page page, string msg, string url)
        {
            url = StringTools.trim(url).Replace("\"", "\\\"");
            url = url.Replace("\\", "\\\\");
            msg = msg.Replace("\n", "\\n");
            string script = "<script language='javascript'>if(confirm('" + msg + "')){"
                          + " window.location=\"" + url + "\";}"
                          + "</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", script);
        }

        /// <summary>
        /// 提示信息后关闭当前页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void checkMsg(System.Web.UI.Page page, string msg)
        {
            string script = "<script language='javascript'>alert('" + msg + "');window.close();</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", script);

        }

        /// <summary>
        /// 确认消息后跳转
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void confirmJump(System.Web.UI.Page page, string url)
        {
            url = StringTools.trim(url).Replace("\"", "\\\"");
            url = url.Replace("\\", "\\\\");
            string script = "<script language='javascript'>"
                          + " window.location=\"" + url + "\";"
                          + "</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "msg", script);
        }


    }
}
