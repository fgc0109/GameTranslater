using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;

using DataManager;

namespace GameTranslaterUI
{
    public partial class MainWindow : Window, SetGlobalData
    {
        internal BasicInfo m_globalBasicInfo = null;

        public void BindingState()
        {
            m_globalBasicInfo = new BasicInfo();

            stateDatabase_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDB") { Source = m_globalBasicInfo });
            stateDataSets_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDS") { Source = m_globalBasicInfo });

            m_globalBasicInfo.InfoDB = "12121212";
            m_globalBasicInfo.InfoDS = "131313131";

            //image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));
        }

        #region 全局数据接口
        public void SetStateDB(bool state)
        {
            m_globalBasicInfo.StateDB = state;
        }
        public void SetStateDS(bool state)
        {
            m_globalBasicInfo.StateDS = state;
        }
        public void SetInfoDB(string info)
        {
            m_globalBasicInfo.InfoDB = info;
        }
        public void SetInfoDS(string info)
        {
            m_globalBasicInfo.InfoDS = info;
        }
        #endregion
    }

    /// <summary>
    /// 提供全局基础信息和事件
    /// </summary>
    public class BasicInfo : INotifyPropertyChanged
    {
        private bool m_stateDatabase = false;
        private bool m_stateDataSets = false;
        private string m_infoDatabase = "m_infoDatabase";
        private string m_infoDataSets = "m_infoDataSets";
        private string m_infoWindow = "m_infoWindow";

        public event PropertyChangedEventHandler PropertyChanged;

        public bool StateDB
        {
            get { return m_stateDatabase; }
            set
            {
                m_stateDatabase = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateDB"));
                }
            }
        }
        public bool StateDS
        {
            get { return m_stateDataSets; }
            set
            {
                m_stateDataSets = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateDS"));
                }
            }
        }
        public string InfoDB
        {
            get { return m_infoDatabase; }
            set
            {
                m_infoDatabase = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoDB"));
                }
            }
        }
        public string InfoDS
        {
            get { return m_infoDataSets; }
            set
            {
                m_infoDataSets = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoDS"));
                }
            }
        }
        public string InfoWin
        {
            get { return m_infoWindow; }
            set
            {
                m_infoWindow = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoWin"));
                }
            }
        }
    }

    class MainData
    {
        static DataSet m_mainDataSet = null;
        static List<DataTable> m_mainDataTable = null;

        public void DropDatas()
        {

        }

        public void Initialize()
        {

        }

        internal void GetDatas()
        {
            for (int i = 0; i < m_mainDataSet.Tables.Count; i++)
            {
                m_mainDataTable.Add(m_mainDataSet.Tables[i]);
            }

        }
    }
}
