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
using UserInfo;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Security.Cryptography;

public partial class Index : AdminPage
{   

    //每页显示的最多记录的条数 
    private int PageSize = 10;

    //当前页号 
    private int CurrentPageNumber;
   
    //显示数据的总条数 
    private int RowCount;

    //总页数 
    private int PageCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (CurrentUser == null || CurrentUser.UserId == "")
        {
            return;
        }
        string userId = CurrentUser.UserId;
        lblUserName.Text = CurrentUser.UserName;
        lblUserID.Text = CurrentUser.UserId;
       
        if (!IsPostBack)
        {
            hdfCurentPage.Value = "1";
            SetBind(1);
        }
        CurrentPageNumber = int.Parse(hdfCurentPage.Value);
    }
    
 

    ///// <summary>
    ///// 登出
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void lbtnLogout_Click(object sender, EventArgs e)
    //{       
    //    LoginHelper.logout(this.Page);
    //    Response.Redirect("Login.aspx");
    //}   

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetBind(1);
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e) 
    { 
        string bgh = e.CommandArgument.ToString();
        string msg;
        bool execResult;
        try
        {
            //判断是否修改类别 
            if (e.CommandName == "Update")
            {
                TextBox tb = e.Item.FindControl("txtKHQRSM") as TextBox;
                execResult = BaseADO.Update_SDSMFWQ_KHSM(bgh, tb.Text.Trim(), out msg);
                if (execResult)
                {
                    ScriptHelper.addMsg(this.Page, "更新成功");
                    SetBind(CurrentPageNumber);
                }
                else
                {
                    ScriptHelper.addMsg(this.Page, "更新失败" + msg);
                }
            }
            else if (e.CommandName == "ConfirmReport")
            {
                execResult = BaseADO.UpdateReportStatus(bgh, 1, out msg);
                if (execResult)
                {
                    ScriptHelper.addMsg(this.Page, msg);
                    SetBind(CurrentPageNumber);
                }
                else
                {
                    ScriptHelper.addMsg(this.Page, "更新状态失败" + msg);
                }
            }
            else if (e.CommandName == "CancelConfirm")
            {
                execResult = BaseADO.UpdateReportStatus(bgh, 0, out msg);
                if (execResult)
                {
                    ScriptHelper.addMsg(this.Page, msg);
                    SetBind(CurrentPageNumber);
                }
                else
                {
                    ScriptHelper.addMsg(this.Page, "更新状态失败" + msg);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptHelper.addMsg(this.Page, "更新状态失败,请稍后再试!" + ex.Message);
            LogHelper.log4netRec(ex, bgh +"执行"+ e.CommandName+"失败");            
        }
    }

    protected void lbtnPage_Command(object sender, CommandEventArgs e)
    {
        int goPageNumber;
        switch (e.CommandName)
        {
            case "First":
                goPageNumber = 1;
                break;
            case "Previous":
                goPageNumber = CurrentPageNumber - 1;
                break;
            case "Next":
                goPageNumber = CurrentPageNumber + 1;
                break;
            case "Last":
                goPageNumber = 10000;
                break;
            default:
                goPageNumber = 1;
                break;
        }
        //UpdateCurrentPageAll();
        SetBind(goPageNumber);
    }

    /// <summary> 
    /// 绑定Repeater 
    /// </summary> 
    private void SetBind(int pageNumber)
    {
        if (CurrentUser == null || CurrentUser.UserId == "")
        {
            LoginHelper.logout(this.Page);
            //回到登陆页面
            Response.Write("<script language=JavaScript>parent.location.href=\"Login.aspx\";</script>");
            return;
        }
        DataTable dtResutlt = BaseADO.GetSdsmAll(CurrentUser.UserId, txtCompanyName.Text.Trim(), ddlOrderBy.SelectedValue, ddlOrderDirection.SelectedValue, ddlStatus.SelectedValue);       
        if (pageNumber <= 0)
        {
            pageNumber = 1;
        }
        RowCount = dtResutlt.Rows.Count;
        if (RowCount % PageSize > 0)
        {
            PageCount = RowCount / PageSize + 1;
        }
        else
        {
            PageCount = RowCount / PageSize;
        }
        CurrentPageNumber = pageNumber;
        if (CurrentPageNumber > PageCount)
        {
            CurrentPageNumber = PageCount;
        }     
        lblMessage.Text = "当前第" + CurrentPageNumber + "/" + PageCount + "页";

        litRecordsCount.Text = RowCount.ToString();

        DataTable dtSource = SplitDataTable(dtResutlt, CurrentPageNumber, PageSize);
        rptList.DataSource = dtSource;
        rptList.DataBind();
        hdfCurentPage.Value = CurrentPageNumber.ToString();
        SetButton();
    }

    private void SetButton()
    {
        lbtnFirst.Enabled = CurrentPageNumber != 1;
        lbtnPrevious.Enabled = CurrentPageNumber != 1;
        lbtnNext.Enabled = CurrentPageNumber != PageCount;
        lbtnLast.Enabled = CurrentPageNumber != PageCount;
    }

    public DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
    {
        if (PageIndex == 0)
            return dt;
        DataTable newdt = dt.Clone();       
        int rowbegin = (PageIndex - 1) * PageSize;
        int rowend = PageIndex * PageSize;

        if (rowbegin >= dt.Rows.Count)
            return newdt;

        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }

        return newdt;
    }
    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            string reportStatus = drv["当前状态"].ToString();            
            Button  updateStatus = (Button)e.Item.FindControl("btnUpdateStatus");            
            if(reportStatus=="等待确认")
            {
                updateStatus.CommandName = "ConfirmReport";
                updateStatus.Text = "确认";
            }
            else if (reportStatus == "等待上传")
            {
                //updateStatus.CommandName = "CancelConfirm";
                //updateStatus.Text = "取消确认";
                updateStatus.Visible = false;
            }
            else
            {
                updateStatus.Visible = false;
            }

            int reportUploadStatus = NumberTools.getInt(drv["网页"].ToString());
            Literal litHref = (Literal)e.Item.FindControl("litHref");
            if (reportUploadStatus == 1)
            {
                litHref.Text = "<a style='color:#06c;' data-title=\"2申报主表\" _href='ExcelDetail.aspx?type=1&Pid=A02&ReportName=" + HttpUtility.UrlEncode(drv["报告号"].ToString(), Encoding.GetEncoding("utf-8")) + "&CompanyName=" + HttpUtility.UrlEncode(drv["公司名称"].ToString(), Encoding.GetEncoding("utf-8")) + "' onclick=\"Hui_admin_tab(this)\" href=\"javascript:;\">查看报表</a>";
            }
            else
            {
                litHref.Text = "查看报表";
            }            
        }
    }
}
