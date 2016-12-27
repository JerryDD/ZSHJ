using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Util;

public partial class _Default : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
          if (CurrentUser == null || CurrentUser.UserId == "")
                return;
            StringBuilder sb = new StringBuilder();
            //DataTable dt = BaseADO.GetPageListFromCache();
            StringBuilder sbPageList = new StringBuilder();

            string strPageName = "";
            string strPageUrl = "";
            string strReportName = "";
            string strCompanyName = "";

            //if (dt.Rows.Count > 0)
            //{
            //    //PageID,PageName,PageInnerOrder,PageTypeID,PageTypeName,PageUrl,Remark                
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        strPageUrl = dt.Rows[i]["PageUrl"].ToString();
            //        strPageName = dt.Rows[i]["PageName"].ToString();
            //        sbPageList.Append("<li><a _href=\"" + strPageUrl + "&ReportName=REPORT_NAME\" href=\"javascript:void(0)\" data-title=\"" + strPageName + "\">&nbsp;&nbsp;&nbsp;" + strPageName + "</a></li>");                    
            //    }
            //}

            //DataTable dtReport = BaseADO.GetUserReportList(CurrentUser.UserId);
       
           

            sb.Append(" <aside class=\"Hui-aside\">");
            //sb.Append("<input runat=\"server\" id=\"divScrollValue\" type=\"hidden\" value=\"\" />");
            sb.Append("     <div class=\"menu_dropdown bk_2\">");

            sb.Append("<dl id=\"menu-search\">");
            sb.Append("     <dt><i class=\"Hui-iconfont\">&#xe62d;</i><a _href=\"Index.aspx\" data-title=\"我的桌面\" href=\"javascript:void(0)\">&nbsp;查询所有</a></dt>");
            sb.Append("</dl>");
            sb.Append("<dl id=\"menu-Report\">");           
            sb.Append("</dl>");

            //if (dtReport.Rows.Count > 0)
            //{
            //    //PageID,PageName,PageInnerOrder,PageTypeID,PageTypeName,PageUrl,Remark
            //    for (int i = 0; i < dtReport.Rows.Count; i++)
            //    {
            //        strCompanyName = dtReport.Rows[i]["公司名称"].ToString();
            //        strReportName = dtReport.Rows[i]["报告号"].ToString();
            //        sb.Append("<dl id=\"menu-baogao\">");
            //        sb.Append("     <dt><i class=\"Hui-iconfont\">&#xe61a;</i> " + strCompanyName + "<i class=\"Hui-iconfont menu_dropdown-arrow\">&#xe6d5;</i></dt>");
            //        sb.Append("         <dd>");
            //        sb.Append("             <ul>");
            //        sb.Append(sbPageList.ToString().Replace("REPORT_NAME", HttpUtility.UrlEncode(strReportName)));                    
            //        sb.Append("         </ul>");
            //        sb.Append("     </dd>");
            //        sb.Append("</dl>");
                   
            //    }
            //}           
            sb.Append("</div>");
            sb.Append("</aside>");
            this.lbMenu.Text = sb.ToString();
        }
    }

    protected void btnQuit_Click(object sender, EventArgs e)
    {
        LoginHelper.logout(this.Page);
        //回到登陆页面
        Response.Write("<script language=JavaScript>parent.location.href=\"Login.aspx\";</script>");
    }
}
