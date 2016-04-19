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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameTranslaterUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindingState();
            listView2.DataContext = GetDataTable();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (m_globalBasicInfo.StateDB == false)
            {
                m_globalBasicInfo.StateDB = true;
                m_globalBasicInfo.InfoDB = m_globalBasicInfo.InfoDB + "aaaa";
            }
            else
            {
                m_globalBasicInfo.StateDB = false;
            }


            textBox.Text = "";
            bool state;


            RuneCalculate basic = new RuneCalculate();
            textBox.Text = basic.Calculate(out state, 2, TextBox_Life.Text, TextBox_Attack.Text, TextBox_Defence.Text, TextBox_Magic.Text);

            List<string[]> info = new List<string[]>();
            string[] MHP = TextBox_Life.Text.Split('\t');
            string[] ATT = TextBox_Attack.Text.Split('\t');

            if (state==false)
            {
                image.Visibility = Visibility.Visible;
                image1.Visibility = Visibility.Hidden;
            }
            else
            {
                image.Visibility = Visibility.Hidden;
                image1.Visibility = Visibility.Visible;
            }
        }
    }
}
