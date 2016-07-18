using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GameTranslaterUI
{
    /// <summary>
    /// 提供全局基础信息和事件
    /// </summary>
    public class BasicInfomation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_stateDatabase = false;
        private bool m_stateDataSets = false;

        private ObservableCollection<string> m_plugList = new ObservableCollection<string>();

        private DataSet m_mainDataSet = new DataSet();
        private DataTable m_mainDataTable = new DataTable();

        /// <summary>
        /// 提供程序数据表加载状态信息
        /// </summary>
        public bool StateDataBase
        {
            get { return m_stateDatabase; }
            set
            {
                m_stateDatabase = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateDataBase"));
            }
        }

        /// <summary>
        /// 提供程序数据集加载状态信息
        /// </summary>
        public bool StateDataSets
        {
            get { return m_stateDataSets; }
            set
            {
                m_stateDataSets = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateDataSets"));
            }
        }

        /// <summary>
        /// 提供插件列表信息
        /// </summary>
        public ObservableCollection<string> InfoPlugList
        {
            get { return m_plugList; }
            set
            {
                m_plugList = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoPlugList"));
            }
        }

        /// <summary>
        /// 主数据集
        /// </summary>
        public DataSet MainDataSet
        {
            get { return m_mainDataSet; }
            set
            {
                m_mainDataSet = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MainDataSet"));
            }
        }

        /// <summary>
        /// 主数据表
        /// </summary>
        public DataTable MainDataTable
        {
            get { return m_mainDataTable; }
            set
            {
                m_mainDataTable = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MainDataTable"));
            }
        }
    }
}
