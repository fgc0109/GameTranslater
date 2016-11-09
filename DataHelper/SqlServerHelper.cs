using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataHelper
{
    /// <summary>
    /// SqlServer数据辅助类
    /// 类变量采用匈牙利命名法
    /// 其他变量采用驼峰命名法
    /// </summary>
    public class SqlServerHelper
    {
        static private SqlConnection m_Connection = null;
        static private SqlDataAdapter m_DataAdapter = null;
        static private SqlCommand m_Command = null;

        static public SqlConnection Connection
        {
            get { return m_Connection; }
        }

        /// <summary>
        /// 连接MySQL数据库
        /// </summary>
        /// <param name="host">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="name">数据库名</param>
        /// <param name="user">数据库用户名</param>
        /// <param name="pass">数据库密码</param>
        /// <param name="exception">异常信息</param>
        /// <returns>连接状态</returns>
        static public bool OpenSql(string host, string port, string user, string pass, string name, out string exception)
        {
            string connectionString = string.Format("Server = {0};port={1};Database = {2}; User ID = {3}; Password = {4};",
                host, port, name, user, pass);
            m_Connection = new SqlConnection(connectionString);
            try
            {
                exception = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                return false;
            }
        }

        static public DataSet ExecuteDataSet(string strCommand, params object[] paraValues)
        {
            DataSet local_dataset = new DataSet();
            try
            {
                m_DataAdapter = new SqlDataAdapter(strCommand, m_Connection);
                m_DataAdapter.Fill(local_dataset);

                return local_dataset;
            }
            catch
            {
                return null;
            }
        }

        static public DataTable ExecuteDataTable(string strCommand, params object[] paraValues)
        {
            DataTable local_datatable = new DataTable();
            try
            {
                m_DataAdapter = new SqlDataAdapter(strCommand, m_Connection);
                m_DataAdapter.Fill(local_datatable);

                return local_datatable;
            }
            catch
            {
                return null;
            }
        }

        static public int ExecuteNonQuery(string strCommand, params object[] paraValues)
        {
            m_Command.Connection = m_Connection;
            m_Command.CommandText = strCommand;
            if (paraValues != null)
            {
                foreach (SqlParameter parm in paraValues)
                    m_Command.Parameters.Add(parm);
            }

            return m_Command.ExecuteNonQuery();
        }
    }
}
