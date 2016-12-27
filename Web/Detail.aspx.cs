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
using Util;
using System.Text;

public partial class Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindData();           
        }
    }

    private void BindData()
    {
        string bgh = Request.QueryString["bgh"] != null ? HttpUtility.UrlDecode(Request.QueryString["bgh"], Encoding.GetEncoding("utf-8")) : "";
        if (!string.IsNullOrEmpty(bgh))
        {
            string status;
            DataTable dt = BaseADO.GetSdsmOne(bgh,out status);
            rptStatus.DataSource = dt;
            rptStatus.DataBind();
            SetProcess(dt);
            litBgh.Text = bgh;
            litStatus.Text = status;
        }
        //int type = NumberTools.getInt(Request.QueryString["type"]);
        //if (type <= 0)
        //{
        //    type = 2;
        //}
        //if (type == 1)
        //{
        //    divProcess.Visible = true;
        //    ordertrack.Visible = false;
        //}
        //else
        //{
        //    divProcess.Visible = false;
        //    ordertrack.Visible = true;
        //}
    }


    private void SetProcess(DataTable dt)
    {
        Literal lit;
        if (dt != null && dt.Rows.Count > 0)
        {
            int ROW_COUNT = dt.Rows.Count;
            string litName;
            for (int i = 1; i <= ROW_COUNT; i++)
            {
                litName = "lit" + i;
                lit = Page.FindControl(litName) as Literal;
                if (lit != null)
                {
                    lit.Text = "<div class='node ready'><ul><li class='tx1'>&nbsp;</li><li class='tx2'>" + dt.Rows[i - 1]["环节名称"].ToString() + "</li><li class='tx3'>" + StringTools.GetDate(dt.Rows[i - 1]["处理时间"].ToString()) + "<br>" + StringTools.GetTime(dt.Rows[i - 1]["处理时间"].ToString()) + "</li></ul></div>";
                    if (i % 5 > 0)
                    {
                        if (i < ROW_COUNT)
                        {
                            lit.Text += "<div class='proce ready'><ul><li class='tx1'>&nbsp;</li></ul></div>";
                        }
                        else
                        {
							if(i != 9)
							{
								lit.Text += "<div class='proce wait'><ul><li class='tx1'>&nbsp;</li></ul></div>";
							}
                        }
                    }
                }                
            }
            if (dt.Rows.Count < 10)
            {
                string statusName;
                for (int i = dt.Rows.Count + 1; i <= 9; i++)
                {	
                    switch (i)
                    {
                        case 1:
                            statusName = "收集资料";
                            break;
                        case 2:
                            statusName = "录入信息";
                            break;
                        case 3:
                            statusName = "等待处理";
                            break;
                        case 4:
                            statusName = "等待审核";
                            break;
                        case 5:
                            statusName = "等待确认";
                            break;
                        case 6:
                            statusName = "等待上传";
                            break;
                        case 7:
                            statusName = "等待打印";
                            break;
                        case 8:
                            statusName = "等待出库";
                            break;
                        case 9:
                            statusName = "报告完成";
                            break;                       
                        default:
                            statusName = "";
                            break;
                          
                    }
                    litName = "lit" + i;
                    lit = Page.FindControl(litName) as Literal;
                    if (lit != null)
                    {
                        lit.Text = "<div class='node wait'><ul><li class='tx1'>&nbsp;</li><li class='tx2'>" + statusName + "</li><li class='tx3'>&nbsp;<br>&nbsp;</li></ul></div>";
                        if (i % 5 > 0)
                        {
							if(i != 9)
							{
								lit.Text += "<div class='proce wait'><ul><li class='tx1'>&nbsp;</li></ul></div>";
							}
                        }
                    }    
                }
            }
        }
    }



    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {      
        LoginHelper.logout(this.Page);
    }
}
