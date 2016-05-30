﻿using System;
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

using System.Drawing;
using DataHelper;
using RuneDesign;

namespace GameTranslaterUI
{
    public partial class MainWindow : Window
    {
        private InfoBasic m_globalBasicInfo = null;
        private DataSet m_mainDataSet = null;
        private List<DataTable> m_mainDataTable = null;

        public void bindingState()
        {
            //添加数据库连接状态改变的事件
            MySqlHelper.OpenMySql("localhost", "3306", "root", "123456", "par_db_new");
            MySqlHelper.Connection.StateChange += M_dbConnection_StateChange;

            m_globalBasicInfo = new InfoBasic();
            m_globalRunesInfo = new InfoRunes();

            stateDatabase_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDB") { Source = m_globalBasicInfo });
            stateDataSets_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDS") { Source = m_globalBasicInfo });

            m_globalBasicInfo.InfoDB = "12121212";
            m_globalBasicInfo.InfoDS = "131313131";

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
                m_globalBasicInfo.StateDB = true;
            }
            else if (MySqlHelper.Connection.State ==ConnectionState.Closed)
            {
                m_globalBasicInfo.StateDB = false;
            }
            else if (MySqlHelper.Connection.State ==ConnectionState.Connecting)
            {      
                m_globalBasicInfo.StateDB = false;
            }
            else
            {
                m_globalBasicInfo.StateDB = false;
            }
           // throw new NotImplementedException();
        }
    }

    
}
