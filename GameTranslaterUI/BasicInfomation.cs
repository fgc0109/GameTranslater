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
        private string m_infoDatabase = "m_infoDatabase";
        private string m_infoDataSets = "m_infoDataSets";
        private ObservableCollection<string> m_plugList = new ObservableCollection<string>();

        public bool StateDB
        {
            get { return m_stateDatabase; }
            set
            {
                m_stateDatabase = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateDB"));
            }
        }
        public bool StateDS
        {
            get { return m_stateDataSets; }
            set
            {
                m_stateDataSets = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateDS"));
            }
        }
        public string InfoDB
        {
            get { return m_infoDatabase; }
            set
            {
                m_infoDatabase = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoDB"));

            }
        }
        public string InfoDS
        {
            get { return m_infoDataSets; }
            set
            {
                m_infoDataSets = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoDS"));
            }
        }

        public ObservableCollection<string> plugListInfo
        {
            get { return m_plugList; }
            set
            {
                m_plugList = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("plugListInfo"));
            }
        }
    }
}
