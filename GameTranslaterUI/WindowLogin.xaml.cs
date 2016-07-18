using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using DataHelper;
using IMainPlug;

namespace GameTranslaterUI
{
    /// <summary>
    /// WindowLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLogin : Window
    {
        public readonly string m_appStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        WindowTrans transWindow = null;

        public WindowLogin()
        {
            InitializeComponent();
            InitializingDefult();

            //状态绑定
            BindingState();
            //打开文件监视器
            SettingPlugsWatcher();

            Dispatcher.Invoke(new Action(() => { m_globalBasicInfo.InfoPlugList = ReflectionMainPlugs.CheckPlugFiles(m_appStartupPath, "ITranslaterInterface"); }));
            Dispatcher.Invoke(new Action(() => { listView_Plugs.SelectedIndex = 0; }));

            listView_Plugs.DataContext = GetDataTable();
        }

        private void InitializingDefult()
        {
            TextBox_Addr.Text = "localhost";
            TextBox_Port.Text = "3306 ";
            TextBox_User.Text = "root";
            TextBox_Pass.Text = "123456";
            TextBox_Base.Text = "refdata";
            TextBox_Table.Text = "border";
        }

        private void plugChangeWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { m_globalBasicInfo.InfoPlugList = ReflectionMainPlugs.CheckPlugFiles(m_appStartupPath, "ITranslaterInterface"); }));
        }

        private void button_LoginIn_Click(object sender, RoutedEventArgs e)
        {
            object[] dataObject = null;

            //根据标签页选择加载数据的方式
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    MySqlHelper.OpenMySql(TextBox_Addr.Text, TextBox_Port.Text, TextBox_Base.Text, TextBox_User.Text, TextBox_Pass.Text);
                    dataObject = MySqlHelper.ExecuteData(DataType.dataTable, TextBox_Table.Text);
                    break;
                case 1:
                    MySqlHelper.OpenMySql(TextBox_Addr.Text, TextBox_Port.Text, TextBox_Base.Text, TextBox_User.Text, TextBox_Pass.Text);
                    dataObject = MySqlHelper.ExecuteData(DataType.dataSet, TextBox_Table.Text);
                    break;
                default:
                    dataObject = new object[] { false, null, "Error" };
                    break;
            }

            //根据插件选择加载的窗口
            switch (tabControl.SelectedIndex)
            {
                case 99:
                    //basicWindow = new WindowRunes(dataObject);
                    //basicWindow.Show();
                    break;
                default:
                    transWindow = new WindowTrans(dataObject);
                    transWindow.Show();

                    //basicWindow = new WindowRunes(dataObject);
                    //basicWindow.Show();
                    break;
            }
            Close();
        }

        private void listView_Plugs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = listView_Plugs.SelectedItem as DataRowView;
            ITranslaterInterface temp = (ITranslaterInterface)ReflectionMainPlugs.LoadAssembly(m_appStartupPath + @"\Plugs\", row.Row[1] as string);
            TextBox_DebugInfo.Text = temp.PlugInfo();

            tabControl.IsEnabled = temp.DataNeeded();
        }
    }
}
