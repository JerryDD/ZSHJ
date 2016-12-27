using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// CommonTools 的摘要说明
/// </summary>
public class CommonTools
{
	public CommonTools()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        
	}

    public static string GetCardType(string type)
    {
        switch(type)
        {         
            case "ctc":
                return "中国电信";
            case "cmcc":
                return "中国移动";
            case "cucc":
                return "中国联通";
            default:
                return "";
        }
    }

    public static string GetCardTypeEnglish(string type)
    {
        switch (type)
        {
            case "电信":
                return "ctc";
            case "移动":
                return "cmcc";
            case "联通":
                return "cucc";
            default:
                return "";
        }
    }


}
