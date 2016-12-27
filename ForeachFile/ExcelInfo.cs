using System;
using System.Collections.Generic;
using System.Text;
public class ExcelInfo
{

    public ExcelInfo() { }

    /// <summary>
    /// 所对应的课程类型
    /// </summary>

    private string fileName;
    public string FileName
    {
        get { return fileName; }
        set { fileName = value; }
    }

    /// <summary>
    /// 书所对应的ISBN号
    /// </summary>
    private DateTime lastModifyTime;
    public DateTime LastModifyTime
    {
        get { return lastModifyTime; }
        set { lastModifyTime = value; }
    }
}
