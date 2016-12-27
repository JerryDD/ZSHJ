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
    /// LogHelper 的摘要说明
    /// </summary>
    public class LogHelper
    {
        public LogHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 把异常记录到log4net的applog中
        /// </summary>
        /// <param name="ex"></param>
        public static void log4netRec(Exception ex)
        {
            log4netToName(ex, "applog");
        }

        /// <summary>
        /// 把异常和附加信息记录到log4net的applog中
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="extMsg"></param>
        public static void log4netRec(Exception ex, string extMsg)
        {
            log4netToName(ex, extMsg, "applog");
        }

        /// <summary>
        /// 把异常记录到log4net的applog中
        /// </summary>
        /// <param name="errmsg"></param>
        public static void log4netRec(String errmsg)
        {
            log4netToName(errmsg, "applog");
        }

        /// <summary>
        /// 把信息记录到指定名称的1og4net的日志中
        /// </summary>
        /// <param name="errmsg"></param>
        /// <param name="logName"></param>
        public static void log4netToName(string errmsg, string logName)
        {
            try
            {
                log4net.LogManager.GetLogger(logName).Error(errmsg);
            }
            catch (Exception e)
            { }
        }

        /// <summary>
        /// 将异常和附加信息记录到指定名称的log4net日志中
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="extMsg"></param>
        /// <param name="logName"></param>
        public static void log4netToName(Exception ex, string extMsg, string logName)
        {
            try
            {
                log4net.LogManager.GetLogger(logName).Error(ex.Message + extMsg, ex);
            }
            catch (Exception e)
            { }
        }

        /// <summary>
        /// 将异常记录到指定名称的log4net日志中
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logName"></param>
        public static void log4netToName(Exception ex, string logName)
        {
            try
            {
                log4net.LogManager.GetLogger(logName).Error(ex.Message, ex);
            }
            catch (Exception e)
            { }
        }
    }
}
