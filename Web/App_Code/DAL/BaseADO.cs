using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Util;
using System.Text;

/// <summary>
/// BeanADO 的摘要说明
/// </summary>
public class BaseADO
{
    /// <summary>
    /// 根据条件，获取报告列表
    /// </summary>
    /// <param name="khbm">客户编码</param>
    /// <param name="gsmc">公司名称</param>
    /// <param name="orderByColumn">排序字段</param>
    /// <param name="orderDirection">排序规则</param>
    /// <param name="status">当前状态</param>
    /// <returns></returns>
    public static DataTable GetSdsmAll(string khbm,string gsmc,string orderByColumn,string orderDirection,string status)
    {
        /*
        * -- =============================================
        ALTER PROCEDURE [dbo].[prWeb_SELECT_ALL_SDSM_FWQ]
	        @khbm VARCHAR(50),				--客户编码
	        @gsmc VARCHAR(200),				--公司名称
	        @chvOrderByColumn VARCHAR(50),	--排序字段
	        @chvOrderDirection VARCHAR(10), --排序规则
	        @chvStatus VARCHAR(50)			--当前状态
        AS
         */
        SqlParameter[] paras ={
            new SqlParameter("@khbm",SqlDbType.VarChar,50),
            new SqlParameter("@gsmc",SqlDbType.VarChar,200),
            new SqlParameter("@chvOrderByColumn",SqlDbType.VarChar,50),
            new SqlParameter("@chvOrderDirection",SqlDbType.VarChar,10),
            new SqlParameter("@chvStatus",SqlDbType.VarChar,50)       
        };
        paras[0].Value = khbm;
        paras[1].Value = gsmc;//(老虎机加豆渠道)
        paras[2].Value = orderByColumn;
        paras[3].Value = orderDirection;
        paras[4].Value = status;
        return SQLHelper.RunProceToDataTable("prWeb_SELECT_ALL_SDSM_FWQ", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());
    }

    /// <summary>
    /// 根据报告号，获取报告的流程轨迹
    /// </summary>
    /// <param name="bgh">报告号</param>
    /// <returns></returns>
    public static DataTable GetSdsmOne(string bgh,out string status)
    {
        /*
        * -- =============================================
         ALTER PROCEDURE [dbo].[prWeb_SELECT_ONE_SDSM_FWQ]
            @bgh VARCHAR(50)   --报告号
        AS
         */
        SqlParameter[] paras ={
            new SqlParameter("@bgh",SqlDbType.VarChar,50)
        };
        paras[0].Value = bgh;

        DataTable dtTemp = SQLHelper.RunProceToDataTable("prWeb_SELECT_ONE_SDSM_FWQ", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());
        return CovertTable2(dtTemp, out status);
    }


     /// <summary>
    /// 把报告信息，按照常规报告生成顺序，一行翻译成多行
    /// </summary>
    /// <param name="dtTemp"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private static DataTable CovertTable2(DataTable dtTemp, out string status)
    {
        StringBuilder sbProcess = new StringBuilder();
        DataTable dtReturn = new DataTable();
        status = "";
        if (dtTemp != null && dtTemp.Rows.Count == 1)
        {
            status = dtTemp.Rows[0]["当前状态"] != null ? dtTemp.Rows[0]["当前状态"].ToString() : "";
            dtReturn.Columns.Add("ID", Type.GetType("System.Int32")); 
            dtReturn.Columns.Add("环节名称", Type.GetType("System.String")); 
            dtReturn.Columns.Add("处理时间", Type.GetType("System.String")); 
            dtReturn.Columns.Add("处理信息", Type.GetType("System.String")); 
            DataRow newRow; 
            DateTime dtParse;

            //当前状态/*用于判断是否已完成，具体时间该显示哪一个*/
            //收集资料,收集资料时间,收集资料说明,
            if(dtTemp.Rows[0]["收集资料"].ToString()=="1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 1;
                newRow["环节名称"] = "收集资料";
                if (dtTemp.Rows[0]["收集资料时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["收集资料时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["收集资料说明"] != null?dtTemp.Rows[0]["收集资料说明"].ToString():"";
                dtReturn.Rows.Add(newRow); 
            }	

            //录入信息,信息录入时间,录入信息说明,
            if (dtTemp.Rows[0]["录入信息"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 2;
                newRow["环节名称"] = "录入信息";
                if (dtTemp.Rows[0]["信息录入时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["信息录入时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["录入信息说明"] != null ? dtTemp.Rows[0]["录入信息说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }
           
            //报告处理,报告处理时间,报告处理说明,
            if (dtTemp.Rows[0]["报告处理"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 3;
                newRow["环节名称"] = "等待处理";
                if (dtTemp.Rows[0]["报告处理时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告处理时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告处理说明"] != null ? dtTemp.Rows[0]["报告处理说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }

            //报告审核,报告审核时间,报告审核说明,
            if (dtTemp.Rows[0]["报告审核"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 4;
                newRow["环节名称"] = "等待审核";
                if (dtTemp.Rows[0]["报告审核时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告审核时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告审核说明"] != null ? dtTemp.Rows[0]["报告审核说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }

            //	客户确认,客户确认时间,客户确认说明,	
            if (dtTemp.Rows[0]["客户确认"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 5;
                newRow["环节名称"] = "等待确认";
                if (dtTemp.Rows[0]["客户确认时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["客户确认时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["客户确认说明"] != null ? dtTemp.Rows[0]["客户确认说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }  
 
             //报告上传,报告上传时间,报告上传说明            
            if (dtTemp.Rows[0]["报告上传"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 6;
                newRow["环节名称"] = "等待上传";
                if (dtTemp.Rows[0]["报告上传时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告上传时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告上传说明"] != null ? dtTemp.Rows[0]["报告审核说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }          

             //报告打印,报告打印时间,报告打印说明,
            if (dtTemp.Rows[0]["报告打印"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 7;
                newRow["环节名称"] = "等待打印";
                if (dtTemp.Rows[0]["报告打印时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告打印时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告打印说明"] != null ? dtTemp.Rows[0]["报告打印说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }

                   
            
            //报告报盘,报告报盘时间,报告报盘说明,
            if (dtTemp.Rows[0]["报告上传"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 8;
                newRow["环节名称"] = "等待出库";
                if (dtTemp.Rows[0]["报告报盘时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告报盘时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告报盘说明"] != null ? dtTemp.Rows[0]["报告报盘说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }
            
            //报告出库,报告出库时间,报告出库说明,
            if (dtTemp.Rows[0]["报告出库"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 9;
                newRow["环节名称"] = "报告完成";
                if (dtTemp.Rows[0]["报告出库时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告出库时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告出库说明"] != null ? dtTemp.Rows[0]["报告出库说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }            
        }
        return dtReturn;
    }



    /// <summary>
    /// 把报告信息，按照常规报告生成顺序，一行翻译成多行
    /// </summary>
    /// <param name="dtTemp"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private static DataTable CovertTable(DataTable dtTemp, out string status)
    {
        StringBuilder sbProcess = new StringBuilder();
        DataTable dtReturn = new DataTable();
        status = "";
        if (dtTemp != null && dtTemp.Rows.Count == 1)
        {
            status = dtTemp.Rows[0]["当前状态"] != null ? dtTemp.Rows[0]["当前状态"].ToString() : "";
            dtReturn.Columns.Add("ID", Type.GetType("System.Int32")); 
            dtReturn.Columns.Add("环节名称", Type.GetType("System.String")); 
            dtReturn.Columns.Add("处理时间", Type.GetType("System.String")); 
            dtReturn.Columns.Add("处理信息", Type.GetType("System.String")); 
            DataRow newRow; 
            DateTime dtParse;

            //当前状态/*用于判断是否已完成，具体时间该显示哪一个*/
            //收集资料,收集资料时间,收集资料说明,
            if(dtTemp.Rows[0]["收集资料"].ToString()=="1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 1;
                newRow["环节名称"] = "收集资料";
                if (dtTemp.Rows[0]["收集资料时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["收集资料时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["收集资料说明"] != null?dtTemp.Rows[0]["收集资料说明"].ToString():"";
                dtReturn.Rows.Add(newRow); 
            }	

            //录入信息,信息录入时间,录入信息说明,
            if (dtTemp.Rows[0]["录入信息"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 2;
                newRow["环节名称"] = "录入信息";
                if (dtTemp.Rows[0]["信息录入时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["信息录入时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["录入信息说明"] != null ? dtTemp.Rows[0]["录入信息说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }

             //数据采集,数据采集时间,数据采集说明,,
            if (dtTemp.Rows[0]["录入信息"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 3;
                newRow["环节名称"] = "数据采集";
                if (dtTemp.Rows[0]["数据采集时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["数据采集时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["数据采集说明"] != null ? dtTemp.Rows[0]["数据采集说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }
            //报告处理,报告处理时间,报告处理说明,
            if (dtTemp.Rows[0]["报告处理"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 4;
                newRow["环节名称"] = "报告处理";
                if (dtTemp.Rows[0]["报告处理时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告处理时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告处理说明"] != null ? dtTemp.Rows[0]["报告处理说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }

             //报告打印,报告打印时间,报告打印说明,
            if (dtTemp.Rows[0]["报告打印"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 5;
                newRow["环节名称"] = "报告打印";
                if (dtTemp.Rows[0]["报告打印时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告打印时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告打印说明"] != null ? dtTemp.Rows[0]["报告打印说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }

            //报告审核,报告审核时间,报告审核说明,
            if (dtTemp.Rows[0]["报告审核"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 6;
                newRow["环节名称"] = "报告审核";
                if (dtTemp.Rows[0]["报告审核时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告审核时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告审核说明"] != null ? dtTemp.Rows[0]["报告审核说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }
            //	客户确认,客户确认时间,客户确认说明,	
            if (dtTemp.Rows[0]["客户确认"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 7;
                newRow["环节名称"] = "客户确认";
                if (dtTemp.Rows[0]["客户确认时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["客户确认时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["客户确认说明"] != null ? dtTemp.Rows[0]["客户确认说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }          
            //报告上传,报告上传时间,报告上传说明            
            if (dtTemp.Rows[0]["报告上传"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 8;
                newRow["环节名称"] = "报告上传";
                if (dtTemp.Rows[0]["报告上传时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告上传时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告上传说明"] != null ? dtTemp.Rows[0]["报告审核说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }           
            //报告报盘,报告报盘时间,报告报盘说明,
            if (dtTemp.Rows[0]["报告上传"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 9;
                newRow["环节名称"] = "报告报盘";
                if (dtTemp.Rows[0]["报告报盘时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告报盘时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告报盘说明"] != null ? dtTemp.Rows[0]["报告报盘说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }
            //报告出库,报告出库时间,报告出库说明,
            if (dtTemp.Rows[0]["报告出库"].ToString() == "1")
            {
                newRow = dtReturn.NewRow();
                newRow["ID"] = 10;
                newRow["环节名称"] = "报告出库";
                if (dtTemp.Rows[0]["报告出库时间"] != null && DateTime.TryParse(dtTemp.Rows[0]["报告出库时间"].ToString(), out dtParse))
                {
                    newRow["处理时间"] = dtParse.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    newRow["处理时间"] = "";
                }
                newRow["处理信息"] = dtTemp.Rows[0]["报告出库说明"] != null ? dtTemp.Rows[0]["报告出库说明"].ToString() : "";
                dtReturn.Rows.Add(newRow);
            }            
        }
        return dtReturn;
    }

    /// <summary>
    /// 根据报告号，更新客户确认说明字段
    /// </summary>
    /// <param name="bgh">报告号</param>
    /// <param name="khqrsm">客户确认说明</param>
    /// <param name="msg">输出结果</param>
    /// <returns>1成功，-1失败</returns>
    public static bool Update_SDSMFWQ_KHSM(string bgh, string khqrsm, out string msg)
    {
        /*        
        ALTER PROCEDURE [dbo].[prWeb_Update_SDSMFWQ_KHSM]
            @bgh VARCHAR(50),			--报告号
            @khqrsm VARCHAR(200),		--客户确认说明
            @chvMsg VARCHAR(50) OUTPUT	--输出结果
         * */

        SqlParameter[] paras ={          
            new SqlParameter("@bgh",SqlDbType.VarChar,50),
            new SqlParameter("@khqrsm",SqlDbType.VarChar,200),           
            new SqlParameter("@chvMsg",SqlDbType.VarChar,50)
        };
        paras[0].Value = bgh;
        paras[1].Value = khqrsm;
        paras[2].Direction = ParameterDirection.Output;

        int re = SQLHelper.RunProcReturn("prWeb_Update_SDSMFWQ_KHSM", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());      
        msg = paras[2].Value.ToString();
        return 1 == re;
    }

    /// <summary>
    /// 用户登陆账号验证，因为一个用户可能对应多个标号，只返回最近一条信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <param name="userName"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static int UserVerify(string userId, string password, out string userName, out string msg)
    {
        /*        
    ALTER PROCEDURE [dbo].[prWeb_LoginV2]
        @khbm VARCHAR(50),	  --客户编码
        @khmm VARCHAR(50),    --客户密码
        @khmc VARCHAR(300) OUTPUT,--客户名称     
        @chvMsg VARCHAR(50) OUTPUT--错误信息
 */

        SqlParameter[] paras ={          
            new SqlParameter("@khbm",SqlDbType.VarChar,50),
            new SqlParameter("@khmm",SqlDbType.VarChar,50),  
            new SqlParameter("@khmc",SqlDbType.VarChar,300),  
            new SqlParameter("@chvMsg",SqlDbType.VarChar,50)
        };
        paras[0].Value = userId;
        paras[1].Value = password;
        paras[2].Direction = ParameterDirection.Output;
        paras[3].Direction = ParameterDirection.Output;

        int re = SQLHelper.RunProcReturn("prWeb_Login", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());
        userName = paras[2].Value.ToString();
        msg = paras[3].Value.ToString();
        return re;
    }

    /// <summary>
    /// 用户登陆账号验证，因为一个用户可能对应多个标号，只返回最近一条信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <param name="userName"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static int UpdatePassword(string userId, string oldPassword, string newPassword, out string msg)
    {
/*        
    ALTER PROCEDURE [dbo].[prWeb_UpdatePassword]
        @khbm VARCHAR(50),	  --客户编码
        @oldPassword VARCHAR(50),    --客户密码
        @newPassword VARCHAR(50)，   --客户新密码     
        @chvMsg VARCHAR(50) OUTPUT--错误信息
 */

        SqlParameter[] paras ={          
            new SqlParameter("@khbm",SqlDbType.VarChar,50),
            new SqlParameter("@oldPassword",SqlDbType.VarChar,50),  
            new SqlParameter("@newPassword",SqlDbType.VarChar,50),  
            new SqlParameter("@chvMsg",SqlDbType.VarChar,50)
        };
        paras[0].Value = userId;
        paras[1].Value = oldPassword;
        paras[2].Value = newPassword;
        paras[3].Direction = ParameterDirection.Output;

        int re = SQLHelper.RunProcReturn("prWeb_UpdatePassword", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());       
        msg = paras[3].Value.ToString();
        return re;
    }

    /// <summary>
    /// 根据客户编号，获取报告列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static DataTable GetUserReportList(string userId)
    {
        /*        
    ALTER PROCEDURE [dbo].[prWeb_GetReportList]
        @khbm VARCHAR(50)	  --客户编码     
 */

        SqlParameter[] paras ={          
            new SqlParameter("@khbm",SqlDbType.VarChar,50)
        };
        paras[0].Value = userId;     

        return SQLHelper.RunProceToDataTable("prWeb_GetReportList", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());        
    }

    /// <summary>
    /// 根据报告号获取报告的详细信息
    /// </summary>
    /// <param name="bgh"></param>
    /// <returns></returns>
    public static DataTable GetSdsmReport(string bgh)
    {
        /*
       =============================================
         ALTER PROCEDURE [dbo].[prWeb_SELECT_SDSM_REPORT]
            @bgh VARCHAR(50)   --报告号
        AS
         */
        SqlParameter[] paras ={
            new SqlParameter("@bgh",SqlDbType.VarChar,50)
        };
        paras[0].Value = bgh;

        return SQLHelper.RunProceToDataTable("prWeb_SELECT_SDSM_REPORT", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());        
    }


    /// <summary>
    /// 获取报告基础配置列表
    /// </summary>
    /// <param name="bgh">报告号</param>
    /// <returns></returns>
    private static DataTable GetPageList()
    {
        /*
        [PageID],[PageName],[PageTypeID],PageTypeName,PageUrl,Remark
        * -- =============================================
         ALTER PROCEDURE [dbo].[prWeb_GetPageList]          
        AS
         */
        return SQLHelper.RunProceToDataTable("prWeb_GetPageList", ConfigurationManager.AppSettings["FwqDB"].ToString());        
    }

    /// <summary>
    /// 从缓存中获取报告基本配置列表信息
    /// </summary>
    /// <returns></returns>
    public static DataTable GetPageListFromCache()
    {
        DataTable dtReturn = new DataTable();
        if (CacheHelper.Get("PageList") != null)
        {
            dtReturn = CacheHelper.Get("PageList") as DataTable;
        }
        else
        {
            dtReturn = GetPageList();           
            CacheHelper.Insert("PageList", dtReturn, 60 * 30);
        }
        return dtReturn;
    }


    /// <summary>
    /// 根据报告号，更新客户确认说明字段
    /// </summary>
    /// <param name="bgh">报告号</param>
    /// <param name="khqrsm">客户确认说明</param>
    /// <param name="msg">输出结果</param>
    /// <returns>1成功，-1失败</returns>
    public static bool UpdateReportStatus(string bgh, int updateStatus, out string msg)
    {
        /*        
        ALTER PROCEDURE [dbo].[prWeb_UpdateReportStatus]
            @bgh VARCHAR(50),			--报告号
            @isConfrimOrCancel int,		--确认（1），取消确认（0）
            @chvMsg VARCHAR(50) OUTPUT	--输出结果
         * */

        SqlParameter[] paras ={          
            new SqlParameter("@bgh",SqlDbType.VarChar,50),
            new SqlParameter("@isConfrimOrCancel",SqlDbType.Int),           
            new SqlParameter("@chvMsg",SqlDbType.VarChar,50)
        };
        paras[0].Value = bgh;
        paras[1].Value = updateStatus;
        paras[2].Direction = ParameterDirection.Output;

        int re = SQLHelper.RunProcReturn("prWeb_UpdateReportStatus", paras, ConfigurationManager.AppSettings["FwqDB"].ToString());
        msg = paras[2].Value.ToString();
        return 1 == re;
    }


}
