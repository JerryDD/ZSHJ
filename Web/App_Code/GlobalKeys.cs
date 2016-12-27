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
/// 全局关键字
/// </summary>
public class GlobalKeys
{
	public GlobalKeys()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 保存附加码的key
    /// </summary>
    public const string KEY_VALID_CODE = "KEY_ZS_VALID_CODE";
    /// <summary>
    /// 用户信息key
    /// </summary>
    public const string KEY_USER_INFO = "KEY_ZS_USER_INFO";

    /// <summary>
    /// 用户EXCEL文件存的路径
    /// </summary>
    public const string FILE_EXCEL_PATH = @"\Report\";

    /// <summary>
    /// EXCEL文件转化为HTML后保存的路径
    /// </summary>
    public const string HTML_SAVE_PATH = @"E:\ZSHJ\HTML\";
}
