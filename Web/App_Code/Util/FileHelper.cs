using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel; 

/// <summary>
/// FileHelper 的摘要说明
/// </summary>
public class ExcelXlsHelper
{
    public ExcelXlsHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region Excel2003
    /// <summary>  
    /// 将Excel文件中的数据读出到DataTable中(xls)  
    /// </summary>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public static DataTable ExcelToTableForXLS(string file)
    {
        DataTable dt = new DataTable();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
            ISheet sheet = hssfworkbook.GetSheetAt(0);

            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueTypeForXLS(header.GetCell(i) as HSSFCell);
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    //continue;  
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as HSSFCell);
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    /// <summary>  
    /// 将DataTable数据导出到Excel文件中(xls)  
    /// </summary>  
    /// <param name="dt"></param>  
    /// <param name="file"></param>  
    public static void TableToExcelForXLS(DataTable dt, string file)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        ISheet sheet = hssfworkbook.CreateSheet("Test");

        //表头  
        IRow row = sheet.CreateRow(0);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            ICell cell = row.CreateCell(i);
            cell.SetCellValue(dt.Columns[i].ColumnName);
        }

        //数据  
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row1 = sheet.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);
                cell.SetCellValue(dt.Rows[i][j].ToString());
            }
        }

        //转为字节数组  
        MemoryStream stream = new MemoryStream();
        hssfworkbook.Write(stream);
        byte[] buf = stream.ToArray();

        //保存为Excel文件  
        using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
        {
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
        }
    }

    /// <summary>  
    /// 获取单元格类型(xls)  
    /// </summary>  
    /// <param name="cell"></param>  
    /// <returns></returns>  
    private static object GetValueTypeForXLS(HSSFCell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank: //BLANK:  
                return null;
            case CellType.Boolean: //BOOLEAN:  
                return cell.BooleanCellValue;
            case CellType.Numeric: //NUMERIC:  
                return cell.NumericCellValue;
            case CellType.String: //STRING:  
                return cell.StringCellValue;
            case CellType.Error: //ERROR:  
                return cell.ErrorCellValue;
            case CellType.Formula: //FORMULA:  
            default:
                return "=" + cell.CellFormula;
        }
    }
    #endregion  

}