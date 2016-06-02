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

            listView2.DataContext = getDataTable();

            comboBox_Plugs.Dispatcher.Invoke(new Action(() => { comboBox_Plugs.ItemsSource = ReflectionMainPlugs.checkPlugFiles(m_appStartupPath); }));
            comboBox_Plugs.Dispatcher.Invoke(new Action(() => { comboBox_Plugs.SelectedIndex = 0; }));


            FileSystemWatcher plugChangeWatcher = new FileSystemWatcher();
            plugChangeWatcher.Path = m_appStartupPath + @"\Plugs\";
            plugChangeWatcher.Filter = "*.dll";
            plugChangeWatcher.EnableRaisingEvents = true;

            plugChangeWatcher.Changed += PlugChangeWatcher_Changed;
        }

        private void PlugChangeWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            comboBox_Plugs.Dispatcher.Invoke(new Action(() => { comboBox_Plugs.ItemsSource = ReflectionMainPlugs.checkPlugFiles(m_appStartupPath); }));
            //comboBox_Plugs.Dispatcher.Invoke(new Action(() => { comboBox_Plugs.SelectedIndex = 0; }));
            //throw new NotImplementedException();
        }

        private void button_LoadAssembly_Click(object sender, RoutedEventArgs e)
        {
            string temp=            ReflectionMainPlugs.LoadAssembly(m_appStartupPath + @"\Plugs\",comboBox_Plugs.SelectedItem as string);

            if (ReflectionMainPlugs.m_plugAssembly!=null)
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

