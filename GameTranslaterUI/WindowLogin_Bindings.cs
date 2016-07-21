using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;

namespace GameTranslaterUI
{
    /// <summary>
    /// 绑定窗口控件数据
    /// </summary>
    public partial class WindowLogin : Window
    {
        public void BindingState()
        {
            //stateDataBase_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDB") { Source = m_globalBasicInfo });
            //stateDataSets_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDS") { Source = m_globalBasicInfo });    
        }

        private void InitializeDefult()
        {
            TextBox_Addr.Text = "localhost";
            TextBox_Port.Text = "3306 ";
            TextBox_User.Text = "root";
            TextBox_Pass.Text = "123456";
            TextBox_Base.Text = "refdata";
            TextBox_Table.Text = "border";
        }

        private DataTable GetDataTable()
        {
            DataTable dataPlugs = new DataTable();

            DataColumn ID = new DataColumn("ID");
            ID.DataType = System.Type.GetType("System.Int32");
            ID.AutoIncrement = true;

            dataPlugs.Columns.Add(ID);
            dataPlugs.PrimaryKey = new DataColumn[1] { ID };

            dataPlugs.Columns.Add(new DataColumn("Name", typeof(string)));
            foreach (var item in mGlobalBasicInfo.InfoPlugList)
            {
                dataPlugs.Rows.Add(null, item);
            }

            return dataPlugs;
        }

        private void connectionState_Change(object sender, StateChangeEventArgs e)
        {
            if (MySqlHelper.Connection.State == ConnectionState.Open)
            {
                mGlobalBasicInfo.StateDataBase = true;
            }
            else if (MySqlHelper.Connection.State == ConnectionState.Closed)
            {
                mGlobalBasicInfo.StateDataBase = false;
            }
            else if (MySqlHelper.Connection.State == ConnectionState.Connecting)
            {
                mGlobalBasicInfo.StateDataBase = false;
            }
            else
            {
                mGlobalBasicInfo.StateDataBase = false;
            }
        }
    }
}
