using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;
using System.IO;
using System;

namespace GameTranslaterUI
{
    public partial class WindowTrans : Window
    {
        /// <summary>
        /// 设置文件监视器
        /// </summary>
        private void SettingPlugsWatcher()
        {
            FileSystemWatcher plugChangeWatcher = new FileSystemWatcher();
            plugChangeWatcher.Path = mAppStartupPath + @"\Plugs\";
            plugChangeWatcher.Filter = "*.dll";
            plugChangeWatcher.EnableRaisingEvents = true;

            plugChangeWatcher.Changed += plugChangeWatcher_Changed;

            Dispatcher.Invoke(new Action(() => { mGlobalBasicInfo.InfoPlugList = ReflectionMainPlugs.CheckPlugFiles(mAppStartupPath, "ITranslaterInterface"); }));
            Dispatcher.Invoke(new Action(() => { listBox_Plugs.SelectedIndex = 0; }));
        }
        private void plugChangeWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { mGlobalBasicInfo.InfoPlugList = ReflectionMainPlugs.CheckPlugFiles(mAppStartupPath, "ITranslaterInterface"); }));
        }

        /// <summary>
        /// 检查插件状态
        /// </summary>
        private void CheckPlugsState()
        {
            
        }

        /// <summary>
        /// 设置插件列表控件的结构和数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            DataTable dataPlugs = new DataTable();

            //DataColumn indexColumn = new DataColumn("ID");
            //indexColumn.DataType = Type.GetType("System.Int32");
            //indexColumn.AutoIncrement = true;
            //indexColumn.AutoIncrementSeed = 1;

            //dataPlugs.Columns.Add(indexColumn);
            //dataPlugs.PrimaryKey = new DataColumn[1] { indexColumn };

            dataPlugs.Columns.Add(new DataColumn("Name", typeof(string)));

            foreach (var item in mGlobalBasicInfo.InfoPlugList)          
                dataPlugs.Rows.Add(item);
            return dataPlugs;
        }
    }
}
