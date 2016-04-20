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
            double min;

            TextBox_Life.Text = (1 / (1 + Slider_ATT.Value) + Slider_LEF.Value).ToString();
            TextBox_Attack.Text = (1 + Slider_ATT.Value).ToString();
            TextBox_Defence.Text = ((1 / (1 + Slider_ATT.Value)) * (1 + Slider_DEF.Value) - (Slider_LEF.Value * 1.6)).ToString();
            TextBox_Magic.Text = ((1 / (1 + Slider_ATT.Value)) * (1 - Slider_DEF.Value) - (Slider_LEF.Value * 1.6)).ToString();

            RuneCalculate basic = new RuneCalculate();
            textBox.Text = basic.Calculate(out state, out min, int.Parse(TextBox_Level.Text)-1, TextBox_Life.Text, TextBox_Attack.Text, TextBox_Defence.Text, TextBox_Magic.Text);

            TextBox_Deviation.Text = min.ToString();

            List<string[]> info = new List<string[]>();

            if (state == false)
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

    public class WindowInfo : INotifyPropertyChanged
    {
        private double m_MHP = 0;
        public double MHP
        {
            get { return m_MHP; }
            set
            {
                m_MHP = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InfoDB"));
                }
            }
        }
    }
}
