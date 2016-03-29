using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace DataManager
{
    /// <summary>
    /// 容纳数据库事件的附加信息
    /// </summary>
    internal class DatabaseEventArgs : EventArgs
    {
        private readonly Boolean m_state;
        private readonly String m_info;

        public DatabaseEventArgs(Boolean state, String info)
        {
            m_state = state;
            m_info = info;
        }

        public Boolean State
        {
            get { return m_state; }
        }
        public String Info
        {
            get { return m_info; }
        }
    }

    /// <summary>
    /// 容纳表格事件的附加信息
    /// </summary>
    internal class ExcelEventArgs : EventArgs
    {
        private readonly Boolean m_state;
        private readonly String m_info;

        public ExcelEventArgs(Boolean state, String info)
        {
            m_state = state;
            m_info = info;
        }

        public Boolean State
        {
            get { return m_state; }
        }
        public String Info
        {
            get { return m_info; }
        }
    }

    /// <summary>
    /// 数据库事件
    /// </summary>
    internal class DatabaseEvents
    {
        public event EventHandler<DatabaseEventArgs> EventCallback;

        protected virtual void OnNewEvent(DatabaseEventArgs e)
        {
            EventHandler<DatabaseEventArgs> temp = Volatile.Read(ref EventCallback);
            if (temp != null)
                temp(this, e);
        }

        public void GetNewEvent(Boolean state, String info)
        {
            DatabaseEventArgs e = new DatabaseEventArgs(state, info);
            OnNewEvent(e);
        }
    }

    /// <summary>
    /// 数据表事件
    /// </summary>
    internal class ExcelEvents
    {
        public event EventHandler<ExcelEventArgs> EventCallback;

        protected virtual void OnNewEvent(ExcelEventArgs e)
        {
            EventHandler<ExcelEventArgs> temp = Volatile.Read(ref EventCallback);
            if (temp != null)
                temp(this, e);
        }

        public void GetNewEvent(Boolean state, String info)
        {
            ExcelEventArgs e = new ExcelEventArgs(state, info);
            OnNewEvent(e);
        }
    }

   
    /// <summary>
    /// 回调函数
    /// </summary>
    internal class Callbacks
    {
        internal void eventDatabase_Ready(object sender, DatabaseEventArgs e)
        {
            string a = Properties.Resource.Database_Ready;
        }
        internal void eventDatabase_Error(object sender, DatabaseEventArgs e)
        {
            string a = Properties.Resource.Database_Error;
        }

        internal void eventExcel_Ready(object sender, ExcelEventArgs e)
        {
            string a = Properties.Resource.Database_Ready;
        }
        internal void eventExcel_Error(object sender, ExcelEventArgs e)
        {
            string a = Properties.Resource.Database_Error;
        }
    }
}
