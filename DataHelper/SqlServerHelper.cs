using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataHelper
{
    public class SqlServerHelper
    {
        static private SqlConnection m_dbConnection = null;
        static private SqlDataAdapter m_dbDataAdapter = null;
        static private SqlCommand m_dbCommand = null;

        static public SqlConnection Connection
        {
            get { return m_dbConnection; }
        }

        static public void OpenSql(string host, string port, string user, string pass, string database)
        {
            string connectionString = string.Format("Server = {0};port={1};Database = {2}; User ID = {3}; Password = {4};",
                host, port, database, user, pass);
            m_dbConnection = new SqlConnection(connectionString);

            if (m_dbConnection.State != ConnectionState.Open)
            {
                try
                {
                    m_dbConnection.Open();
                }
                catch
                {

                }
            }
        }

        static public DataSet ExecuteDataSet(string strCommand, params object[] paraValues)
        {
            DataSet local_dataset = new DataSet();
            try
            {
                m_dbDataAdapter = new SqlDataAdapter(strCommand, m_dbConnection);
                m_dbDataAdapter.Fill(local_dataset);

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
                m_dbDataAdapter = new SqlDataAdapter(strCommand, m_dbConnection);
                m_dbDataAdapter.Fill(local_datatable);

                return local_datatable;
            }
            catch
            {
                return null;
            }
        }

        static public int ExecuteNonQuery(string strCommand, params object[] paraValues)
        {
            m_dbCommand.Connection = m_dbConnection;
            m_dbCommand.CommandText = strCommand;
            if (paraValues != null)
            {
                foreach (SqlParameter parm in paraValues)
                    m_dbCommand.Parameters.Add(parm);
            }

            return m_dbCommand.ExecuteNonQuery();
        }
    }
}
