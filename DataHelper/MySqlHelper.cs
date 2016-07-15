using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataHelper
{
    public enum DataType
    {
        dataSet,
        dataTable
    }

    static public class MySqlHelper
    {
        static private MySqlConnection m_dbConnection = null;
        static private MySqlDataAdapter m_dbDataAdapter = null;
        static private MySqlCommand m_dbCommand = null;

        static public MySqlConnection Connection
        {
            get { return m_dbConnection; }
        }

        /// <summary>
        /// 连接MySQL数据库
        /// </summary>
        /// <param name="host">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="name">数据库名</param>
        /// <param name="user">数据库用户名</param>
        /// <param name="pass">数据库密码</param>
        /// <returns>返回连接标识,连接状态枚举,错误信息</returns>
        static public object[] OpenMySql(string host, string port, string name, string user, string pass)
        {
            string connectionString = string.Format("Server = {0};port={1};Database = {2}; User ID = {3}; Password = {4};",
                host, port, name, user, pass);
            m_dbConnection = new MySqlConnection(connectionString);
            try
            {
                m_dbConnection.Open();

                if (m_dbConnection.State == ConnectionState.Open)
                    return new object[3] { true, m_dbConnection.State, string.Empty };
                else
                    return new object[3] { false, m_dbConnection.State, string.Empty };
            }
            catch (Exception ex)
            {
                return new object[3] { false, m_dbConnection.State, ex };
            }
        }

        /// <summary>
        /// 返回对应名称的数据集或数据表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paraValues"></param>
        /// <returns>返回数据状态标识,数据集或数据表,错误信息</returns>
        static public object[] ExecuteData(DataType type, params object[] paraValues)
        {
            DataSet local_data = new DataSet();
            string strCommand = "select * from {0};";
            try
            {
                m_dbDataAdapter = new MySqlDataAdapter(string.Format(strCommand, paraValues), m_dbConnection);
                m_dbDataAdapter.Fill(local_data);

                return new object[3] { true, local_data, string.Empty };
            }
            catch (Exception ex)
            {
                return new object[3] { false, null, ex };
            }
        }

        static public int ExecuteNonQuery(string strCommand, params object[] paraValues)
        {
            m_dbCommand.Connection = m_dbConnection;
            m_dbCommand.CommandText = string.Format(strCommand, paraValues);
            if (paraValues != null)
            {
                foreach (MySqlParameter parm in paraValues)
                    m_dbCommand.Parameters.Add(parm);
            }
            return m_dbCommand.ExecuteNonQuery();
        }
    }
}
