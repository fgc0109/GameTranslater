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

            mGlobalBasicInfo.MainDataTable = paraValues[1] as DataTable;

            DataRowView row = listView_Plugs.SelectedItem as DataRowView;
            loadedPlug = (ITranslaterInterface)ReflectionMainPlugs.LoadAssembly(mAppStartupPath + @"\Plugs\", row.Row[1] as string);
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

        private void listView_Plugs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loadedPlug.DataNeeded() == true && mGlobalBasicInfo.MainDataSet == null)
            {
                //该插件不支持导出空数据
            }
        }
    }
}
