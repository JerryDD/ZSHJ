using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModifyPassword : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpdatePwd_Click(object sender, EventArgs e)
    {
        if (CurrentUser == null || CurrentUser.UserId == "")
        {
            LoginHelper.logout(this.Page);
            //回到登陆页面
            Response.Write("<script language=JavaScript>parent.location.href=\"Login.aspx\";</script>");
            return;
        }
        string strOldPassword = this.txtOldPassword.Text.Trim();
        string strNewPassword = this.txtNewPassword.Text.Trim();
        string strConfirmPassword = this.txtConfirmPassword.Text.Trim();

        if (strOldPassword == "" || strNewPassword == "")
        {
            Response.Write("<script>alert('密码不能为空,请输入')</script>");
            return;
        }

        if (strOldPassword == strNewPassword)
        {
            Response.Write("<script>alert('新密码不能与旧密码相同,请重新输入')</script>");
            return;
        }

        if (strNewPassword != strConfirmPassword)
        {
            Response.Write("<script>alert('两次输入的密码不同,请重新输入确认密码')</script>");
            return;
        }

        string msg;
        int intResult = BaseADO.UpdatePassword(CurrentUser.UserId, strOldPassword, strNewPassword, out msg);

        if (intResult == 1)
        {
            //回到首页面
            Response.Write("<script>alert('恭喜您，密码修改成功!')</script>");
            //Response.Write("<script language=JavaScript>parent.location.href=\"Login.aspx\";</script>");
            return;
        }

        else
        {
            Response.Write("<script>alert('"+msg+"')</script>");
            return;
        }
    }
}