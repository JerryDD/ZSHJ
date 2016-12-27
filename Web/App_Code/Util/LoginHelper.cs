using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Util;
using UserInfo;
using System.Text;

/// <summary>
/// Summary description for LoginHelper
/// </summary>
public class LoginHelper
{
	public LoginHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 校验附加码，校验后会将session中的附加码清空
    /// </summary>
    /// <param name="code">待校验的附加码</param>
    /// <returns></returns>
    public static bool checkValidCode(System.Web.UI.Page page, string code)
    {
        if (StringTools.isBlank(code) || page.Session[GlobalKeys.KEY_VALID_CODE] == null)
        {
            page.Session.Remove(GlobalKeys.KEY_VALID_CODE);
            return false;
        }
        bool result = false;
        string currentCode = Convert.ToString(page.Session[GlobalKeys.KEY_VALID_CODE]);
        currentCode = currentCode.ToLower();
        code = code.ToLower();
        if (currentCode.Equals(code))
        {
            result = true;
        }
        page.Session.Remove(GlobalKeys.KEY_VALID_CODE);
        return result;
    }



    /// <summary>
    /// 判断是否已经登陆,包含统一验证
    /// </summary>
    /// <param name="page"></param>        
    /// <returns>如果已经登陆则返回用户信息，否则返回null</returns>
    public static UserCopy hasLogin(System.Web.UI.Page page)
    {
        UserCopy user = null;
        user = (UserCopy)page.Session[GlobalKeys.KEY_USER_INFO];
        if (user != null)
        {
            return user;
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// 登陆成功后初始化系统环境，如将用户信息放入session中
    /// </summary>
    /// <param name="page"></param>
    private static void initEnv(System.Web.UI.Page page, UserCopy usercopy)
    {
         //保存到session
         page.Session[GlobalKeys.KEY_USER_INFO] = usercopy;
         return;
    }

    /// <summary>
    /// 清除环境变量
    /// </summary>
    /// <param name="page"></param>
    private static void clearEnv(System.Web.UI.Page page)
    {
        page.Session.Remove(GlobalKeys.KEY_USER_INFO);
    }

    /// <summary>
    /// 登陆 返回users对象,并保存到session中
    /// </summary>
    /// <param name="page"></param>
    /// <param name="userId"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="msg"></param>
    /// <returns>登陆失败返回null否则返回users对象,并保存到session中</returns>
    public static UserCopy Login(System.Web.UI.Page page, string userId, string password, out string msg)
    {
        try
        {
            string userName = "";
            long result = BaseADO.UserVerify(userId, password, out userName, out msg);
            if (result > 0)
            {
                UserCopy usercopy = new UserCopy();
                usercopy.UserId = userId;
                usercopy.UserName = userName;

                initEnv(page, usercopy);

                msg = "登陆成功";
                return usercopy;
            }
            else
            {
                if (StringTools.isBlank(msg))
                    msg = "登陆失败！";
                return null;
            }
        }
        catch (Exception ex)
        {
            msg = "数据库操作失败，请重试。如果问题仍然存在，请联系系统管理人员。谢谢!!!";
            return null;
        }
    }

    /// <summary>
    /// 注销用户
    /// </summary>
    /// <param name="page"></param>
    public static void logout(System.Web.UI.Page page)
    {
        clearEnv(page);
        page.Session.Abandon();       
    }

    /// <summary>
    /// 登陆后跳回本页面，使用时应注意不要造成死循环
    /// </summary>
    /// <param name="page"></param>
    public static void gotoLogin(System.Web.UI.Page page)
    {
        string toUrl = page.Request.Url.ToString();
        gotoLogin(page, toUrl);
    }

    /// <summary>
    /// 跳转到登陆页面
    /// </summary>
    /// <param name="page"></param>    
    /// <param name="toUrl"></param>
    public static void gotoLogin(System.Web.UI.Page page, string toUrl)
    {
        gotoLocalLogin(page, toUrl);
        //gotoOuterLogin(page, toUrl);
    }

    /// <summary>
    /// 跳转到登陆页面
    /// </summary>
    /// <param name="page"></param>    
    /// <param name="toUrl"></param>
    public static void gotoLocalLogin(System.Web.UI.Page page, string toUrl)
    {
        //page.Server.Transfer("~/Login.aspx");
        if (StringTools.isBlank(toUrl))
        {
            string reqUrl = page.Request.RawUrl;
            toUrl = reqUrl;
        }
        toUrl = HttpUtility.UrlEncode(toUrl, Encoding.GetEncoding("utf-8"));
        page.Server.Transfer("~/Login.aspx?toUrl=" + toUrl);      
    }
}
