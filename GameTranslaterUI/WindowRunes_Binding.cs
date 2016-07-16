using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;
using RuneDesign;

namespace GameTranslaterUI
{
    public partial class WindowRunes : Window
    {
        private BasicInfomation m_globalBasicInfo = null;
        private DataSet m_mainDataSet = null;
        private List<DataTable> m_mainDataTable = null;

        public void bindingState()
        {
            //添加数据库连接状态改变的事件
            MySqlHelper.OpenMySql("localhost", "3306", "root", "123456", "par_db_new");
            MySqlHelper.Connection.StateChange += M_dbConnection_StateChange;

            m_globalBasicInfo = new BasicInfomation();
            m_globalRunesInfo = new InfoRunes();

            stateDatabase_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDB") { Source = m_globalBasicInfo });
            stateDataSets_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDS") { Source = m_globalBasicInfo });
            comboBox_Plugs.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("plugListInfo") { Source = m_globalBasicInfo });


            //image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));            
        }

        internal void getDatas()
        {
            for (int i = 0; i < m_mainDataSet.Tables.Count; i++)
            {
                m_mainDataTable.Add(m_mainDataSet.Tables[i]);
            }
        }

        private DataTable getDataTable()
        {
            DataTable data = new DataTable("MyDataTable");

            DataColumn ID = new DataColumn("ID");//第一列
            ID.DataType = System.Type.GetType("System.Int32");
            //ID.AutoIncrement = true; //自动递增ID号 
            data.Columns.Add(ID);

            //设置主键
            DataColumn[] keys = new DataColumn[1];
            keys[0] = ID;
            data.PrimaryKey = keys;

            data.Columns.Add(new DataColumn("Name", typeof(string)));//第二列
            data.Columns.Add(new DataColumn("Age", typeof(string)));//第三列

            data.Rows.Add(1, "  XiaoM", "  20");
            data.Rows.Add(2, "  XiaoF", "  122");
            data.Rows.Add(3, "  XiaoA", "  29");
            data.Rows.Add(4, "  XiaoB", "  102");
            return data;
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
