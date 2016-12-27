using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Diagnostics;
using System.Collections;


    /// <summary>
    /// SQLHelper 的摘要说明。
    /// </summary>
public sealed class SQLHelper
{
    ///   <summary>   
    ///   建立command等待被其他存储过程调用.   
    ///   </summary>   
    ///   <param   name="procName">存储过程名称</param>
    ///   <param   name="prams">存储过程参数.</param>
    ///   <returns>返回Command</returns>   
    public static SqlCommand MakeCommand(string procedureName, SqlParameter[] parameters, SqlConnection sqlCon)
    {
        SqlCommand cm = new SqlCommand();
        cm.CommandText = procedureName;
        cm.CommandType = CommandType.StoredProcedure;
        cm.Connection = sqlCon;
        if (parameters != null)
        {
            foreach (SqlParameter sqlparam in parameters)
                cm.Parameters.Add(sqlparam);
        }
        SqlParameter returnParameter = new SqlParameter("ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cm.Parameters.Add(returnParameter);
        return cm;
    }

    /// <summary>
    /// 封装SqlParameter
    /// </summary>
    /// <param name="ParamName">参数名称</param>
    /// <param name="DbType">参数数据类型</param>
    /// <param name="Size">类型大小</param>
    /// <param name="Direction">参数类型</param>
    /// <param name="Value">值</param>
    /// <returns>SqlParameter</returns>
    public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
    {
        SqlParameter param;

        if (Size > 0)
            param = new SqlParameter(ParamName, DbType, Size);
        else
            param = new SqlParameter(ParamName, DbType);

        param.Direction = Direction;
        if (!(Direction == ParameterDirection.Output && Value == null))
            param.Value = Value;
        return param;
    }

    ///   <summary>   
    ///   运行存储过程(不带参数)   
    ///   </summary>   
    ///   <param   name="procName">存储过程名称</param>   
    ///   <returns>返回存储过程的返回值</returns>   
    public static int RunProcReturn(string proccedureName, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            sqlCon.Open();
            SqlCommand sqlCom = MakeCommand(proccedureName, null, sqlCon);
            sqlCom.ExecuteNonQuery();
            return (int)sqlCom.Parameters["ReturnValue"].Value;
        }
    }

    /// <summary>
    /// 运行存储过程
    /// </summary>
    /// <param name="proccedureName">存储过程名称</param>
    /// <param name="parameter">参数</param>
    /// <returns>存储过程返回值</returns>
    public static int RunProcReturn(string proccedureName, SqlParameter[] parameter, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            sqlCon.Open();
            SqlCommand sqlCom = MakeCommand(proccedureName, parameter, sqlCon);
            int num = sqlCom.ExecuteNonQuery();
            return (int)sqlCom.Parameters["ReturnValue"].Value;
        }
    }

    ///   <summary>
    ///   运行存储过程(不带参数)   
    ///   </summary>
    ///   <param name="procName">存储过程名称</param>
    ///   <returns>返回ExecuteNonQuery值</returns>
    public static int RunProccedure(string proccedureName, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            sqlCon.Open();
            SqlCommand sqlCom = MakeCommand(proccedureName, null, sqlCon);
            return sqlCom.ExecuteNonQuery();
        }

    }

    /// <summary>
    /// 运行存储过程
    /// </summary>
    /// <param name="proccedureName">存储过程名称</param>
    /// <param name="parameter">参数</param>
    /// <returns>返回ExecuteNonQuery值</returns>
    public static int RunProccedure(string proccedureName, SqlParameter[] parameter, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            sqlCon.Open();
            SqlCommand sqlCom = MakeCommand(proccedureName, parameter, sqlCon);
            return sqlCom.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// 运行存储过程
    /// </summary>
    /// <param name="proccedureName">存储过程名称</param>
    /// <param name="parameter">参数</param>
    /// <returns>返回ExecuteScalar值</returns>
    public static string RunProc_ExecuteScalar(string proccedureName, SqlParameter[] parameter, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            sqlCon.Open();
            SqlCommand sqlCom = MakeCommand(proccedureName, parameter, sqlCon);
            return sqlCom.ExecuteScalar().ToString();
        }
    }

    ///   <summary>   
    ///   运行存储过程(不带参数)返回DataTable
    ///   <param   name="procName">存储过程名称</param>   
    ///   <returns>DataTable</returns>
    ///   </summary>   
    public static DataTable RunProceToDataTable(string procedureName, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            SqlCommand sqlCom = MakeCommand(procedureName, null, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCom);
            DataSet Myds = new DataSet();
            da.Fill(Myds);
            return Myds.Tables[0];
        }
    }

    ///   <summary>   
    ///   运行存储过程(带参数)返回DataTable   
    ///   <param   name="procName">存储过程名称</param>   
    ///   <param   name="prams">参数集合</param>   
    ///   <returns>DataTable</returns>  
    ///   </summary>   
    public static DataTable RunProceToDataTable(string procedureName, SqlParameter[] parameters, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            SqlCommand sqlCom = MakeCommand(procedureName, parameters, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCom);
            DataSet Myds = new DataSet();
            da.Fill(Myds);
            return Myds.Tables[0];
        }
    }

    ///   <summary>   
    ///   运行存储过程(不带参数)返回DataSet   
    ///   <param   name="procName">存储过程名称</param>   
    ///   <returns>dataset</returns> 
    ///   </summary>   
    public static DataSet RunProceToDataSet(string procedureName, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            SqlCommand sqlCom = MakeCommand(procedureName, null, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCom);
            DataSet Myds = new DataSet();
            da.Fill(Myds);
            return Myds;
        }
    }

    ///   <summary>   
    ///   运行存储过程(带参数)返回DataSet   
    ///   <param   name="procName">存储过程名称</param>   
    ///   <param   name="prams">参数集合</param>   
    ///   <returns>dataset</returns>
    ///   </summary>   
    public static DataSet RunProceToDataSet(string procedureName, SqlParameter[] parameters, string connectionString)
    {
        using (SqlConnection sqlCon = new SqlConnection(connectionString))
        {
            SqlCommand sqlCom = MakeCommand(procedureName, parameters, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCom);
            DataSet Myds = new DataSet();
            da.Fill(Myds);
            return Myds;
        }
    }
}

