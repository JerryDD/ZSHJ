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
/// Summary description for AdminPage
/// </summary>
public class AdminPage : System.Web.UI.Page
{  
    private UserInfo.UserCopy curUser;

    public UserInfo.UserCopy CurrentUser
    {
        get 
        {
            return curUser;
        }
        set
        {
            curUser = value;
        }
    }


    public AdminPage()
    {
        this.Load += new EventHandler(AdminPage_Load);
    }  

    void AdminPage_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string userId;
        curUser = LoginHelper.hasLogin(this.Page);

        if (curUser == null || string.IsNullOrEmpty(curUser.UserId))
        {
            LoginHelper.logout(this.Page);
            //»Øµ½µÇÂ½Ò³Ãæ
            Response.Write("<script language=JavaScript>parent.location.href=\"Login.aspx\";</script>");
            return;
        }
        else
        {
            userId = curUser.UserId;
        }        
    }
}
