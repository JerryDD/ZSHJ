using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Util;
using UserInfo;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string msg;
        UserCopy usercopy = LoginHelper.Login(this.Page, txtEmail.Text.Trim(), txtPassword.Text.Trim(), out msg);
        if (usercopy == null)
        {
            ScriptHelper.addMsg(this.Page, msg);
        }
        else {
            string toUrl;
            if (Request.QueryString["toUrl"] != null && !string.IsNullOrEmpty(Request.QueryString["toUrl"]))
            {
                toUrl = Request.QueryString["toUrl"];
            }
            else
            {
                toUrl = "Default.aspx";
            }
            LogHelper.log4netRec(txtEmail.Text + "登陆成功");
            ScriptHelper.confirmJump(this.Page, toUrl);
        }

    }
}
