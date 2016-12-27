<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
        SystemSetting.initAppEnv(Application);       
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码
        // 在出现未处理的错误时运行的代码
        //Exception ex = Server.GetLastError().GetBaseException();
        //string name = ex.Source;
        //Util.LogHelper.log4netRec(ex);
        //string url = string.Empty;
        //if (ex is System.InvalidOperationException)
        //{
        //    if (Util.StringTools.trim(ex.Message).IndexOf("超时") != -1)
        //    {
        //        string msg = "系统繁忙，请几分钟后再试。如有疑问，请联系客服人员。";
        //        //MessagePage.aspx错误页面一定不能抛出异常，会造成死循环
        //        url = "~/MessagePage.aspx?msg=" + HttpUtility.UrlEncode(msg, Encoding.GetEncoding("utf-8"));
        //    }
        //}
        //if (ex is HttpRequestValidationException)
        //{
        //    HttpRequestValidationException vex = (HttpRequestValidationException)ex;
        //    string msg = "您的输入中含有非法的字符:[" + Util.StringTools.getWebFormat(vex.Message) + "]";
        //    url = "~/MessagePage.aspx?msg=" + HttpUtility.UrlEncode(msg, Encoding.GetEncoding("utf-8"));
        //}
        //if (!string.IsNullOrEmpty(url))
        //    Response.Redirect(url);
        //else
        //{
        //    url = "~/MessagePage.aspx?msg=" + HttpUtility.UrlEncode("系统出错，请检查后重新操作。", Encoding.GetEncoding("utf-8"));
        //}
        //Response.Redirect(url);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    } 
</script>
