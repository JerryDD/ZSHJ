using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using Util;

/// <summary>
/// Summary description for ExcelPreview
/// </summary>
public class ExcelPreview
{

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);  
    public static void Excel2Html(System.Web.UI.Page p, string inFilePath, string outDirPath,string outFileHtmlName)
    {
        Microsoft.Office.Interop.Excel.Application excel = null;
        Microsoft.Office.Interop.Excel.Workbook xls = null;
        excel = new Microsoft.Office.Interop.Excel.Application();
        object missing = Type.Missing;
        object trueObject = true;
        excel.Visible = false;
        excel.DisplayAlerts = false;

        //string randomName = outFileHtmlName;  //output fileName

        xls = excel.Workbooks.Open(inFilePath, missing, trueObject, missing,
                                    missing, missing, missing, missing, missing, missing, missing, missing,
                                    missing, missing, missing);

        //Save Excel to Html
        object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml;
        Workbook wsCurrent = xls;//(Workbook)wsEnumerator.Current;
        String outputFile = outDirPath + outFileHtmlName + ".html";
        try
        {
            wsCurrent.SaveAs(outputFile, format, missing, missing, missing,
                              missing, XlSaveAsAccessMode.xlNoChange, missing,
                              missing, missing, missing, missing);           

            string cssPath = outDirPath + outFileHtmlName + ".files\\stylesheet.css";
            ReplaceText(cssPath);
        }
        catch (Exception ex)
        {
            LogHelper.log4netRec(ex, outputFile + "Save Exception");
        }
        xls.Close(false, missing, missing); 
        excel.Workbooks.Close();
        excel.Quit();
        excel = null;
        xls = null;
        GC.Collect();
        KillExcel(excel);
    }

    private static void KillExcel(Microsoft.Office.Interop.Excel._Application excel)
    {
        try
        {
            Process[] ps = Process.GetProcesses();
            IntPtr t = new IntPtr(excel.Hwnd); //得到这个句柄，具体作用是得到这块内存入口  
            int ExcelID = 0;
            GetWindowThreadProcessId(t, out ExcelID); //得到本进程唯一标志k  
            foreach (Process p in ps)
            {
                if (p.ProcessName.ToLower().Equals("excel"))
                {
                    if (p.Id == ExcelID)
                    {
                        p.Kill();
                        LogHelper.log4netRec("Kill Excel +1");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.log4netRec(ex,"Kill Excel Exception");
        }
    }  

    /// <summary>
    /// 替换某个文本文件中的特定内容
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    public static void ReplaceText(string path)
    {
        string oldValue = ".5pt";
        string newValue = "1pt";
        string text = string.Empty;
        using (StreamReader reader = new StreamReader(path, Encoding.ASCII))
        {
            text = reader.ReadToEnd();
            reader.Close();
        }
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.Write(text.Replace(oldValue, newValue));
            writer.Close();
        }
    }

}