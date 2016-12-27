using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testZip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGen_Click(object sender, EventArgs e)
    {
        ZipOut zipOut = new ZipOut();
        string strZipPath = @"E:\工作内容\2015\TGY\TGYAdmin\Zip\中盛汇缴[2015]第0452号.Zip";
        string strZipTopDirectoryPath = @"E:\工作内容\2015\TGY\TGYAdmin\中盛汇缴[2015]第0452号";
        int intZipLevel = 6;
        string strPassword = "";
        string[] filesOrDirectoriesPaths = new string[] { @"E:\工作内容\2015\TGY\TGYAdmin\中盛汇缴[2015]第0452号" };
        zipOut.Zip(strZipPath, strZipTopDirectoryPath, intZipLevel, strPassword, filesOrDirectoriesPaths);
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Zip/中盛汇缴[2015]第0452号.Zip");
    }
}