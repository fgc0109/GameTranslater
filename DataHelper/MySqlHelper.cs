using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataHelper
{
    static public class MySqlHelper
    {
        static private MySqlConnection mDatabaseConnection = null;
        static private MySqlDataAdapter mDatabaseDataAdapter = null;
        static private MySqlCommand mDatabaseCommand = null;

        static public MySqlConnection Connection
        {
            get { return mDatabaseConnection; }
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
            mDatabaseConnection = new MySqlConnection(connectionString);
            try
            {
                mDatabaseConnection.Open();

                if (mDatabaseConnection.State == ConnectionState.Open)
                    return new object[3] { true, mDatabaseConnection.State, string.Empty };
                else
                    return new object[3] { false, mDatabaseConnection.State, connectionString };
            }
            catch (Exception ex)
            {
                return new object[3] { false, mDatabaseConnection.State, ex };
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
                mDatabaseDataAdapter = new MySqlDataAdapter(string.Format(strCommand, name), mDatabaseConnection);
                mDatabaseDataAdapter.Fill(localData);

                return new object[3] { true, localData, string.Empty };
            }
            catch (Exception ex)
            {
                return new object[3] { false, null, ex };
            }
        }

        static public int ExecuteNonQuery(string strCommand, params object[] paraValues)
        {
            mDatabaseCommand.Connection = mDatabaseConnection;
            mDatabaseCommand.CommandText = string.Format(strCommand, paraValues);
            if (paraValues != null)
            {
                foreach (MySqlParameter parm in paraValues)
                    mDatabaseCommand.Parameters.Add(parm);
            }
            return mDatabaseCommand.ExecuteNonQuery();
        }
    }
}
