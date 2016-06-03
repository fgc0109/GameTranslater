using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace GameTranslaterUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string m_appStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        public MainWindow()
        {
            InitializeComponent();

            //状态绑定
            bindingState();
            //符文绑定
            bindingRunes();
            //打开文件监视器
            settingPlugsWatcher();

            listView2.DataContext = getDataTable();

            ReflectionMainPlugs.InterfaceName = "ITranslaterInterface";

            //线程调度器委托获取可用组件列表
            Dispatcher.Invoke(new Action(() => { m_globalBasicInfo.plugListInfo = ReflectionMainPlugs.checkPlugFiles(m_appStartupPath); }));
            Dispatcher.Invoke(new Action(() => { comboBox_Plugs.SelectedIndex = 0; }));
        }

        private void PlugChangeWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { m_globalBasicInfo.plugListInfo = ReflectionMainPlugs.checkPlugFiles(m_appStartupPath); }));
            Dispatcher.Invoke(new Action(() => { comboBox_Plugs.SelectedIndex = 0; }));
            //throw new NotImplementedException();
        }

        private void button_LoadAssembly_Click(object sender, RoutedEventArgs e)
        {
            string temp = ReflectionMainPlugs.LoadAssembly(m_appStartupPath + @"\Plugs\", comboBox_Plugs.SelectedItem as string);

            if (ReflectionMainPlugs.m_plugAssembly != null)
            {
                textBox.Text = ReflectionMainPlugs.m_plugAssembly.GetTypes().Length.ToString() + "\r\n";
                foreach (var item in ReflectionMainPlugs.m_plugAssembly.GetTypes())
                {
                    textBox.Text += item.FullName + "\r\n";
                }
                textBox.Text += temp;
            }
            else
            {
                textBox.Text = "未发现可用程序集\r\n";
            }
        }
    }
}

