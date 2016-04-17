using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace DataHelper
{
    public class ExcelHelper
    {
        static private OleDbConnection m_dbConnection = null;
        static private OleDbDataAdapter m_dbDataAdapter = null;
        static private OleDbCommand m_dbCommand = null;

        static public OleDbConnection Connection
        {
            get { return m_dbConnection; }
        }

        static public void OpenExcel(string filePath)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + filePath
                + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";

            if (m_dbConnection.State!= ConnectionState.Open)
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
                m_dbDataAdapter = new OleDbDataAdapter(strCommand, m_dbConnection);
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
                m_dbDataAdapter = new OleDbDataAdapter(strCommand, m_dbConnection);
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
                foreach (OleDbParameter parm in paraValues)
                    m_dbCommand.Parameters.Add(parm);
            }

            return m_dbCommand.ExecuteNonQuery();
        }
    }
}
