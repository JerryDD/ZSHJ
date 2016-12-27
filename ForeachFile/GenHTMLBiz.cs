using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace ForeachFile
{
    public class GenHTMLBiz
    {        

        #region 生成HTML文件，更新XML文件
        /// <summary>
        /// 遍历服务文件夹，生成对应的HTML文件
        /// </summary>
        public static void GenHTML()
        {
            //string excelDirectoryName = @"E:\ZSHJ\Web\Report\";            
            //string htmlDirectoryName =  @"E:\ZSHJ\Web\HTML\";
            string excelDirectoryName = ConfigurationManager.AppSettings["ExcelDirectroy"].ToString();
            string htmlDirectoryName = ConfigurationManager.AppSettings["HTMLDirectroy"].ToString();


            if (!Directory.Exists(htmlDirectoryName))
            {
                Directory.CreateDirectory(htmlDirectoryName);
            }
            if (Directory.Exists(excelDirectoryName))
            {
                XmlDocument xmlDoc = LoadXML();
                //所有子文件夹               
                DirectoryInfo reportDirInfo;
                FileInfo fileinfo;
                string clientName, reportNo, htmlFilePath, htmlDocName;
                string fileLastWriteTime, lastModifyTime;
                int i = 0, j = 0;
                string log = "";
                foreach (string item in Directory.GetDirectories(excelDirectoryName))
                {
                    i++;
                    reportDirInfo = new DirectoryInfo(item);
                    clientName = reportDirInfo.Name;
                    //所有子文件
                    foreach (string fileItem in Directory.GetFiles(reportDirInfo.FullName))
                    {
                        j++;
                        fileinfo = new FileInfo(fileItem);
                        lastModifyTime = GetLastModifyDateTime(ref  xmlDoc, fileinfo.Name);
                        fileLastWriteTime = fileinfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                        reportNo = fileinfo.Name.Substring(fileinfo.Name.IndexOf('.') - 3, 3);
                        if (fileLastWriteTime != lastModifyTime)
                        {
                            htmlDocName = reportNo + ".html";
                            htmlFilePath = htmlDirectoryName + clientName.Replace('[', '_').Replace(']', '_') + "\\";
                            if (!Directory.Exists(htmlFilePath))
                            {
                                Directory.CreateDirectory(htmlFilePath);
                            }
                            if (!System.IO.File.Exists(htmlFilePath + htmlDocName))
                            {
                                ExcelPreview.Excel2Html(fileinfo.FullName, htmlFilePath, htmlDocName);
                            }
                            UpdateNode(ref xmlDoc, fileinfo.Name, fileLastWriteTime);
                        }
                    }
                    log += "第" + i + "个文件夹，共处理了" + j + "个文件";
                }
                SaveXML(xmlDoc);
            }
        }

        /// <summary>
        /// 获取XML文件 
        /// </summary>
        /// <returns></returns>
        public static XmlDocument LoadXML()
        {
            string xmlPath = ConfigurationManager.AppSettings["XMLPath"].ToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            return xmlDoc;
        }

        /// <summary>
        /// 保存xml文件
        /// </summary>
        /// <param name="xmlDoc"></param>
        public static void SaveXML(XmlDocument xmlDoc)
        {
            xmlDoc.Save(@"E:\工作内容\2015活动\ForeachFile\ExcelInfo.xml");
        }

        /// <summary>
        /// 更新文件的最近更新日期
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="fileName"></param>
        /// <param name="modifyTime"></param>
        public static void UpdateNode(ref XmlDocument xmlDoc, string fileName, string modifyTime)
        {
            string strPath = string.Format("/ExcelList/Excel [@FileName=\"{0}\"]", fileName);
            XmlElement selectXe = (XmlElement)xmlDoc.SelectSingleNode(strPath);
            if (selectXe == null)
            {
                //得到根节点Students
                XmlNode root = (XmlElement)xmlDoc.SelectSingleNode("ExcelList");
                XmlElement record = xmlDoc.CreateElement("Excel");
                record.SetAttribute("FileName", fileName);
                record.SetAttribute("LastModifyTime", modifyTime);
                root.AppendChild(record);
            }
            else
            {
                selectXe.SetAttribute("LastModifyTime", modifyTime);
            }
        }

        /// <summary>
        /// 获取文件的最新更新日期
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetLastModifyDateTime(ref XmlDocument xmlDoc, string fileName)
        {
            string strPath = string.Format("/ExcelList/Excel [@FileName=\"{0}\"]", fileName);
            XmlElement selectXe = (XmlElement)xmlDoc.SelectSingleNode(strPath);
            if (selectXe != null)
            {
                string nodeValue = selectXe.GetAttribute("LastModifyTime");
                return nodeValue;
            }
            return "";
        }
        #endregion
    }
}
