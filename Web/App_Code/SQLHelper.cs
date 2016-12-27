using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Diagnostics;
using System.Collections;


    /// <summary>
    /// SQLHelper ��ժҪ˵����
    /// </summary>
public sealed class SQLHelper
{
    ///   <summary>   
    ///   ����command�ȴ��������洢���̵���.   
    ///   </summary>   
    ///   <param   name="procName">�洢��������</param>
    ///   <param   name="prams">�洢���̲���.</param>
    ///   <returns>����Command</returns>   
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
    /// ��װSqlParameter
    /// </summary>
    /// <param name="ParamName">��������</param>
    /// <param name="DbType">������������</param>
    /// <param name="Size">���ʹ�С</param>
    /// <param name="Direction">��������</param>
    /// <param name="Value">ֵ</param>
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
    ///   ���д洢����(��������)   
    ///   </summary>   
    ///   <param   name="procName">�洢��������</param>   
    ///   <returns>���ش洢���̵ķ���ֵ</returns>   
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
    /// ���д洢����
    /// </summary>
    /// <param name="proccedureName">�洢��������</param>
    /// <param name="parameter">����</param>
    /// <returns>�洢���̷���ֵ</returns>
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
    ///   ���д洢����(��������)   
    ///   </summary>
    ///   <param name="procName">�洢��������</param>
    ///   <returns>����ExecuteNonQueryֵ</returns>
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
    /// ���д洢����
    /// </summary>
    /// <param name="proccedureName">�洢��������</param>
    /// <param name="parameter">����</param>
    /// <returns>����ExecuteNonQueryֵ</returns>
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
    /// ���д洢����
    /// </summary>
    /// <param name="proccedureName">�洢��������</param>
    /// <param name="parameter">����</param>
    /// <returns>����ExecuteScalarֵ</returns>
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
    ///   ���д洢����(��������)����DataTable
    ///   <param   name="procName">�洢��������</param>   
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
    ///   ���д洢����(������)����DataTable   
    ///   <param   name="procName">�洢��������</param>   
    ///   <param   name="prams">��������</param>   
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
    ///   ���д洢����(��������)����DataSet   
    ///   <param   name="procName">�洢��������</param>   
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
    ///   ���д洢����(������)����DataSet   
    ///   <param   name="procName">�洢��������</param>   
    ///   <param   name="prams">��������</param>   
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

