using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    string outputDirPath = @"E:\文件\"; //Word和Excel转换成Html，Html文件存放的位置
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnWord_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~") + @"\DemoFiles\Test.docx";
        WordPreview.Priview(this, filePath, outputDirPath);
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~") + @"\DemoFiles\Test.xlsx";
        ExcelPreview.Priview(this, filePath, outputDirPath);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~") + @"\DemoFiles\Test.pdf";

        PDFPreview.Priview(this, filePath);
    }

    protected void btnTxt_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~") + @"\DemoFiles\Test.txt";

        TextFilePreview.Preview(this, filePath);
    }
}