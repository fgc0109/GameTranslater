using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataHelper
{
    /// <summary>
    /// MySql数据辅助类
    /// 类变量采用匈牙利命名法
    /// 其他变量采用驼峰命名法
    /// </summary>
    static public class MySqlHelper
    {
        static private MySqlConnection m_Connection = null;
        static private MySqlDataAdapter m_DataAdapter = null;
        static private MySqlCommand m_Command = null;

        static public MySqlConnection Connection
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
        static public bool OpenMySql(string host, string port, string name, string user, string pass, out string exception)
        {
            string connectionString = string.Format("Server = {0};port={1};Database = {2}; User ID = {3}; Password = {4};",
                host, port, name, user, pass);
            m_Connection = new MySqlConnection(connectionString);
            try
            {
                m_Connection.Open();

                exception = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 返回对应名称的数据表
        /// </summary>
        /// <param name="name">数据表名称</param>
        /// <returns>返回数据状态标识,数据表,错误信息</returns>
        static public object[] ExecuteData(string name)
        {
            DataTable localData = new DataTable();
            string strCommand = "select * from {0};";
            try
            {
                m_DataAdapter = new MySqlDataAdapter(string.Format(strCommand, name), m_Connection);
                m_DataAdapter.Fill(localData);

                return new object[3] { true, localData, string.Empty };
            }
            catch (Exception ex)
            {
                return new object[3] { false, null, ex };
            }
        }

        static public int ExecuteNonQuery(string strCommand, params object[] paraValues)
        {
            m_Command.Connection = m_Connection;
            m_Command.CommandText = string.Format(strCommand, paraValues);
            if (paraValues != null)
            {
                foreach (MySqlParameter parm in paraValues)
                    m_Command.Parameters.Add(parm);
            }
            return m_Command.ExecuteNonQuery();
        }
    }
}
