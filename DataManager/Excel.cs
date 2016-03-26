using System;
using System.Data;
using System.Data.OleDb;

namespace DataManager
{
    public class Excel
    {
        static OleDbConnection dbConnection;

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="host">主机地址</param>
        /// <param name="port">数据端口</param>
        /// <param name="user">用户名称</param>
        /// <param name="pass">用户密码</param>
        /// <param name="database">数据库名称</param>
        static public void OpenExcel(string filePath)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + filePath
                + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";

            ExcelEvents events = new ExcelEvents();

            string message = "";
            bool state = true;
            
            try
            {
                dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();

                state = true;
                message = "";

                events.EventCallback += new Callbacks().eventExcel_Ready;
                events.GetNewEvent(state, message);
            }
            catch (Exception er)
            {
                state = false;
                message = er.Message.ToString();

                events.EventCallback += new Callbacks().eventExcel_Error;
                events.GetNewEvent(state, message);
            }
            finally
            {
                events.EventCallback -= new Callbacks().eventExcel_Ready;
                events.EventCallback -= new Callbacks().eventExcel_Error;
            }
        }

        /// <summary>
        /// 加载DataSet
        /// </summary>
        /// <param name="select_string">数据选择字符串</param>
        /// <returns>DataSet</returns>
        static public DataSet LoadExcel(string select_string)
        {
            string strSelect = select_string;

            DataSet local_dataset = new DataSet();
            OleDbDataAdapter local_adapter = new OleDbDataAdapter(strSelect, dbConnection);

            local_adapter.Fill(local_dataset, "data$");

            return local_dataset;
        }
    }
}
