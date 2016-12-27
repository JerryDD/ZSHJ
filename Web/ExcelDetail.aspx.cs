using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

public partial class ExcelDetail : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int fromType;
        fromType = NumberTools.getInt(Request.QueryString["Type"]);
        string reportName;
        string reportNo;
        string rootPath = Server.MapPath("~/");
        if (!rootPath.EndsWith("\\"))
        {
            rootPath = rootPath + "\\";
        }
        //报表导出
        if (fromType == 2)
        {
            if (Session["ReportName"] == null || Session["ReportNo"] == null)
            {
                ScriptHelper.addMsg(this.Page, "请选择报告后再下载");
                return;
            }            
            reportName = Session["ReportName"].ToString();
            reportNo = Session["ReportNo"].ToString();
			string  exportExcelFilePath;
			int fType = NumberTools.getInt(Request["ftype"]);
			if (fType == 1)
            {
				exportExcelFilePath = rootPath + "Report\\" + reportName + "\\" + reportName + reportNo + ".xls";                
            }
            else
            {
				//ZipOut zipOut = new ZipOut();
                //string strZipPath = rootPath + "Zip\\" + reportName + ".Zip";
                //string strZipTopDirectoryPath = rootPath + "Report\\" + reportName;
                //int intZipLevel = 6;
                //string strPassword = "";
                //string[] filesOrDirectoriesPaths = new string[] { strZipTopDirectoryPath };
                //zipOut.Zip(strZipPath, strZipTopDirectoryPath, intZipLevel, strPassword, filesOrDirectoriesPaths);
                //Response.Redirect("Zip\\"+reportName+".Zip");
				exportExcelFilePath = rootPath + "Report\\" + reportName + "\\" + reportName + "ALL.xls";                
            }
			if (!System.IO.File.Exists(exportExcelFilePath))
			{
				ScriptHelper.addMsg(this.Page, "报告还没有生成");
				return;
			}
			if (fType == 1)
            {
				Response.Redirect("Report\\" + reportName + "\\" + reportName + reportNo + ".xls");  
			}
			else
			{
				Response.Redirect("Report\\" + reportName + "\\" + reportName + "ALL.xls");  
			}
            return;
        }
        reportName = HttpUtility.UrlDecode(Request.QueryString["ReportName"]);
        reportNo = Request.QueryString["Pid"];
        Session["ReportName"] = reportName;        
        
        string htmlClientFolderName = reportName.Replace('[', '_').Replace(']', '_');

        if (string.IsNullOrEmpty(reportName) || string.IsNullOrEmpty(reportNo))
        {
            ScriptHelper.addMsg(this.Page, "参数不全");
            return;
        }
        else
        {
            Session["ReportNo"] = reportNo;
        }
        string fileName = reportNo;
        string excelDirectoryName = rootPath + "Report\\" + reportName + "\\";
        string excelFilePath = rootPath + "Report\\" + reportName + "\\" + reportName + fileName + ".xls";
        string htmlDirectoryName = rootPath + "HTML\\" + htmlClientFolderName + "\\";
        string htmlFilePath = rootPath + "HTML\\" + htmlClientFolderName + "\\" + fileName + ".html";


        if (!System.IO.File.Exists(excelFilePath))
        {
            ScriptHelper.addMsg(this.Page, "报告还没有生成");
            return;
        }
        if (System.IO.Directory.Exists(htmlDirectoryName) == false)//如果不存在就创建file文件夹
        {
            System.IO.Directory.CreateDirectory(htmlDirectoryName);
        }

        //if (!System.IO.File.Exists(htmlFilePath))
        {
            string outputDirPath = rootPath + @"HTML\" + htmlClientFolderName + "\\"; //Word和Excel转换成Html，Html文件存放的位置
            ExcelPreview.Excel2Html(this, excelFilePath, outputDirPath, fileName);
        }
        //formType 为1 的时候，为右边结果打开左边菜单
        if (fromType == 1)
        {           
            string companyName = "";
            if (Request.QueryString["CompanyName"]!=null)
            {
                companyName = HttpUtility.UrlDecode(Request.QueryString["CompanyName"]); ;
            }          
            ScriptHelper.addScript(this.Page, "Hui_Menu_Change(\"" + companyName + "\",\"" + reportName + "\")");
            return;
        }
        Response.Redirect("HTML\\" + htmlClientFolderName + "\\" + fileName + ".html");
    }
}