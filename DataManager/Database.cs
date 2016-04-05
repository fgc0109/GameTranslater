﻿using System;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace DataManager
{
    public class Database
    {
        static MySqlConnection m_dbConnection;
        static MySqlDataAdapter m_dbDataAdapter;

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="host">主机地址</param>
        /// <param name="port">数据端口</param>
        /// <param name="user">用户名称</param>
        /// <param name="pass">用户密码</param>
        /// <param name="database">数据库名称</param>
        static public void OpenDatabase(string host, string port, string user, string pass, string database)
        {
            string connectionString = string.Format("Server = {0};port={1};Database = {2}; User ID = {3}; Password = {4};",
                host, port, database, user, pass);

            DatabaseEvents events = new DatabaseEvents();

            string message = "";
            bool state = true;

            try
            {
                m_dbConnection = new MySqlConnection(connectionString);
                m_dbConnection.Open();

                state = true;
                message = "";

                events.EventCallback += new Callbacks().eventDatabase_Ready;
                events.GetNewEvent(state, message);
            }
            catch (Exception er)
            {
                state = false;
                message = er.Message.ToString();

                events.EventCallback += new Callbacks().eventDatabase_Error;
                events.GetNewEvent(state, message);
            }
            finally
            {
                events.EventCallback -= new Callbacks().eventDatabase_Ready;
                events.EventCallback -= new Callbacks().eventDatabase_Error;
            }
        }

        /// <summary>
        /// 加载DataSet
        /// </summary>
        /// <param name="select_string">数据选择字符串</param>
        /// <returns>DataSet</returns>
        static public DataSet LoadDatabase(string select_string)
        {
            if (m_dbConnection.State == ConnectionState.Open)
            {

            }

            string strSelect = select_string;

            DataSet local_dataset = new DataSet();
            MySqlDataAdapter local_adapter = new MySqlDataAdapter(strSelect, m_dbConnection);

            local_adapter.Fill(local_dataset);

            return local_dataset;
        }



        /// <summary>
        /// 将内存数据保存至数据库
        /// </summary>
        /// <param name="update_string">数据库命令字符串</param>
        static public void SaveDatabase(string update_string)
        {
            DatabaseEvents events = new DatabaseEvents();
            //events.EventCallback += new Callbacks().eventDatabase_Info;

            string message = "";
            bool state = true;

            try
            {
                MySqlCommand command = m_dbConnection.CreateCommand();
                command.CommandText = update_string;
                command.ExecuteNonQuery();

                state = true;
                message = Properties.Resource.Database_Ready;
                events.GetNewEvent(state, message);
            }
            catch (Exception er)
            {
                state = false;
                message = er.Message.ToString();
                events.GetNewEvent(state, message);
            }
            finally
            {
                //events.EventCallback -= new Callbacks().eventDatabase_Info;
            }
        }

    }
}
