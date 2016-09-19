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
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using DataHelper;
using IMainPlug;

namespace GameTranslaterUI
{
    /// <summary>
    /// WindowTrans.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTrans : Window
    {
        public readonly string mAppStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        private BasicInfomation mGlobalBasicInfo = null;
        private ITranslaterInterface loadedPlug = null;

        public WindowTrans(params object[] paraValues)
        {
            InitializeComponent();
            mGlobalBasicInfo = new BasicInfomation();

            BindingState();
            SettingPlugsWatcher();

            //listBox_Plugs.DataContext = GetDataTable();
            //listView_Plugs.DataContext = GetDataTable();

            mGlobalBasicInfo.MainDataTable = paraValues[1] as DataTable;

            if (listBox_Plugs.SelectedItem != null)
                loadedPlug = (ITranslaterInterface)ReflectionMainPlugs.LoadAssembly(mAppStartupPath + @"\Plugs\", listBox_Plugs.SelectedItem as string);
        }

        private void button_Export_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "IDX - File(*.idx) | *.idx| 所有文件(*.*) | *.*";
            openFile.ShowDialog();
            //filePath.Text = openFile.FileName.Replace(".idx", "");
            //loadedPlug.FileImport()
        }

        private void button_Import_Click(object sender, RoutedEventArgs e)
        {

        }



        private void listBox_Plugs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = listBox_Plugs.SelectedItem as DataRowView;
            if (listBox_Plugs.SelectedItem != null)
                loadedPlug = (ITranslaterInterface)ReflectionMainPlugs.LoadAssembly(mAppStartupPath + @"\Plugs\", listBox_Plugs.SelectedItem as string);

           

            //if (loadedPlug.DataNeeded() == true && mGlobalBasicInfo.MainDataSet == null)
            //{
            //    //该插件不支持导出空数据
            //}
        }

        private void button_PlugInfo_Click(object sender, RoutedEventArgs e)
        {
            if (loadedPlug != null)
                textBox.Text += loadedPlug.PlugInfo();
        }
    }
}
