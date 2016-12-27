using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
/// <summary>
/// SystemSetting 的摘要说明
/// </summary>
public class SystemSetting
{
	public SystemSetting()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 装载应用程序环境
    /// </summary>
    /// <param name="application"></param>
    public static void initAppEnv(HttpApplicationState application)
    {
        //加载log4net             
        log4net.Config.XmlConfigurator.Configure();
    }       
}
