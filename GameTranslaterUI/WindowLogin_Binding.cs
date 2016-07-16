using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;
using RuneDesign;

namespace GameTranslaterUI
{
    /// <summary>
    /// 绑定窗口控件数据
    /// </summary>
    public partial class WindowLogin : Window
    {
        private BasicInfomation m_globalBasicInfo = null;
        private List<DataTable> m_mainDataTable = null;
        private DataSet m_mainDataSet = null;

        /// <summary>
        /// 绑定全局状态数据
        /// </summary>
        public void bindingState()
        {
            //添加数据库连接状态改变的事件
            MySqlHelper.OpenMySql("localhost", "3306", "root", "123456", "par_db_new");
            MySqlHelper.Connection.StateChange += M_dbConnection_StateChange;

            m_globalBasicInfo = new BasicInfomation();
            
            comboBox_Plugs.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("InfoPlugList") { Source = m_globalBasicInfo });

            //image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));            
        }

        internal void getDatas()
        {
            for (int i = 0; i < m_mainDataSet.Tables.Count; i++)
            {
                m_mainDataTable.Add(m_mainDataSet.Tables[i]);
            }
        }

   

        private void M_dbConnection_StateChange(object sender, StateChangeEventArgs e)
        {
            if (MySqlHelper.Connection.State ==ConnectionState.Open)
            {
                m_globalBasicInfo.StateDataBase = true;
            }
            else if (MySqlHelper.Connection.State ==ConnectionState.Closed)
            {
                m_globalBasicInfo.StateDataBase = false;
            }
            else if (MySqlHelper.Connection.State ==ConnectionState.Connecting)
            {      
                m_globalBasicInfo.StateDataBase = false;
            }
            else
            {
                m_globalBasicInfo.StateDataBase = false;
            }
           // throw new NotImplementedException();
        }
    }

    
}
