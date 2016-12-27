using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for TextFilePreview
/// </summary>
public class TextFilePreview
{
    public static void Preview(System.Web.UI.Page p, string inFilePath)
    {
        string fileName = inFilePath.Substring(inFilePath.LastIndexOf('\\') + 1);

        p.Response.ContentType = "text/plain";
        p.Response.ContentEncoding = System.Text.Encoding.UTF8;  //保持和文件的编码格式一致
        p.Response.AddHeader("content-disposition", "filename=" + fileName);
        p.Response.WriteFile(inFilePath);
        p.Response.End();
    }
}