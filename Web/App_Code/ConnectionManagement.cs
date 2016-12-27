using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// ConnectnionManagement 的摘要说明
/// </summary>
public class ConnectionManagement
{
	public ConnectionManagement()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetFWQConnection()
    {
        return "Data source=180.97.69.95;Initial Catalog=basedb_fwq;User ID=FwqWebUser;Password=FwqWebUserpt@int7Yc;";       
    }
}
