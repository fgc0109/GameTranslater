using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace DataHelper
{
    /// <summary>
    /// Excel数据辅助类
    /// 类变量采用匈牙利命名法
    /// 其他变量采用驼峰命名法
    /// </summary>
    static public class ExcelHelper
    {
        static private OleDbConnection m_Connection = null;
        static private OleDbDataAdapter m_DataAdapter = null;
        static private OleDbCommand m_Command = null;

        static public OleDbConnection Connection
        {
            get { return m_Connection; }
        }

        /// <summary>
        /// 连接Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="engines">Excel对象接口引擎</param>
        /// <param name="version">Excel对象文件版本</param>
        /// <param name="exception">异常信息</param>
        /// <returns>连接状态</returns>
        static private bool OpenExcel(string filePath, ExcelEngines engines, ExcelVersion version, out string exception)
        {
            StringBuilder connectBuilder = new StringBuilder();

            if (engines == ExcelEngines.JET)
                connectBuilder.Append("Provider=Microsoft.Jet.OleDb.4.0;");
            if (engines == ExcelEngines.ACE)
                connectBuilder.Append("Provider=Microsoft.Ace.OleDb.12.0;");

            connectBuilder.Append($"Data Source={filePath};");

            if (version == ExcelVersion.Office1997 || version == ExcelVersion.Office2000 || version == ExcelVersion.Office2002 || version == ExcelVersion.Office2003)
                connectBuilder.Append($"Extended Properties='Excel 8.0; HDR=Yes; IMEX=1;'");
            if (version == ExcelVersion.Office2007)
                connectBuilder.Append($"Extended Properties='Excel 12.0; HDR=Yes; IMEX=1;'");

            m_Connection = new OleDbConnection(connectBuilder.ToString());
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
        /// 获取由多个数据表组成的数据集
        /// </summary>
        /// <param name="tableNames">数据表名称</param>
        /// <param name="exception">异常信息</param>
        /// <returns>数据集数据</returns>
        static public DataSet ExecuteDataSet(string[] tableNames, out string exception)
        {
            DataSet excelDatas = new DataSet();

            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append("select * from ");

            foreach (var item in tableNames)
            {
                commandBuilder.Append($"[{item}$],");
            }
            //移除最后的逗号并替换为分号
            commandBuilder.Remove(commandBuilder.Length - 1, 1).Append(";");

            try
            {
                m_DataAdapter = new OleDbDataAdapter(commandBuilder.ToString(), m_Connection);
                m_DataAdapter.Fill(excelDatas);

                exception = string.Empty;
            }
            catch (Exception ex) { exception = ex.ToString(); }
            return excelDatas;
        }

        /// <summary>
        /// 获取单个数据表
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="exception">异常信息</param>
        /// <returns>数据表数据</returns>
        static public DataTable ExecuteDataTable(string tableName, out string exception)
        {
            DataTable excelData = new DataTable();
            try
            {
                m_DataAdapter = new OleDbDataAdapter($"select * from [{tableName}$];", m_Connection);
                m_DataAdapter.Fill(excelData);

                exception = string.Empty;
            }
            catch (Exception ex) { exception = ex.ToString(); }
            return excelData;
        }

        static public int ExecuteNonQuery(string strCommand, params object[] paraValues)
        {
            m_Command.Connection = m_Connection;
            m_Command.CommandText = strCommand;
            if (paraValues != null)
            {
                foreach (OleDbParameter parm in paraValues)
                    m_Command.Parameters.Add(parm);
            }

            return m_Command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Excel对象接口引擎
    /// </summary>
    public enum ExcelEngines
    {
        JET = 4,
        ACE = 12,
    }

    /// <summary>
    /// Excel对象文件版本
    /// </summary>
    public enum ExcelVersion
    {
        Office1997 = 8,
        Office2000 = 9,
        Office2002 = 10,
        Office2003 = 11,
        Office2007 = 12,
    }
}
