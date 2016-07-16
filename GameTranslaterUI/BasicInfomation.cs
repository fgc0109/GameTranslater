using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
