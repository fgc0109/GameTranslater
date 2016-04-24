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
        private InfoRunes m_globalRunesInfo = null;


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

            double min;
            double deviation = 0;

            //获取符文偏好设定
            bool[] state = new bool[panel_Runes.Children.Count];
            for (int i = 0; i < panel_Runes.Children.Count; i++)
            {
                if (panel_Runes.Children[i] is CheckBox)
                {
                    CheckBox checkbox = panel_Runes.Children[i] as CheckBox;
                    state[i] = (bool)checkbox.IsChecked;
                }
            }

            //获取最佳符文组合
            double[][] InfoReturn = new double[16][];
            for (int i = 0; i < 16; i++)
            {
                RuneCalculate basic = new RuneCalculate();
                InfoReturn[i] = basic.Calculate( state, out min, i, TextBox_MHP.Text, TextBox_ATT.Text, TextBox_DEF.Text, TextBox_RES.Text);
                deviation = deviation + min;
                basic = null;
            }
            deviation = deviation / 16;

            InfoRunes infoRunesFormat = new InfoRunes();
            StringBuilder outputInfoBuilder = new StringBuilder(4096);

            //以权重值方式导出
            if ((bool)radioButton_Format_0.IsChecked)
            {
                textBox.Text = textBox.Text + infoRunesFormat.ReturnWeights(InfoReturn);
            }

            //以品质名方式导出
            if ((bool)radioButton_Format_1.IsChecked)
            {
                textBox.Text = textBox.Text + infoRunesFormat.ReturnQuality(InfoReturn);
            }

            //以符文组方式导出
            if ((bool)radioButton_Format_2.IsChecked)
            {
                textBox.Text = textBox.Text + infoRunesFormat.ReturnGroups(InfoReturn);
            }

            TextBox_Deviation.Text = deviation.ToString();

            //if (state == false)
            //{
            //    image.Visibility = Visibility.Visible;
            //    image1.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    image.Visibility = Visibility.Hidden;
            //    image1.Visibility = Visibility.Visible;
            //}
        }

        private void Slider_LEF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m_globalRunesInfo.MHP = (1 / (1 + Slider_ATT.Value) + Slider_LEF.Value);
            m_globalRunesInfo.ATT = (1 + Slider_ATT.Value);
            m_globalRunesInfo.DEF = ((1 / (1 + Slider_ATT.Value)) * (1 + Slider_DEF.Value) - (Slider_LEF.Value * 1.6));
            m_globalRunesInfo.RES = ((1 / (1 + Slider_ATT.Value)) * (1 - Slider_DEF.Value) - (Slider_LEF.Value * 1.6));
        }

        private void Slider_ATT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m_globalRunesInfo.MHP = (1 / (1 + Slider_ATT.Value) + Slider_LEF.Value);
            m_globalRunesInfo.ATT = (1 + Slider_ATT.Value);
            m_globalRunesInfo.DEF = ((1 / (1 + Slider_ATT.Value)) * (1 + Slider_DEF.Value) - (Slider_LEF.Value * 1.6));
            m_globalRunesInfo.RES = ((1 / (1 + Slider_ATT.Value)) * (1 - Slider_DEF.Value) - (Slider_LEF.Value * 1.6));
        }

        private void Slider_DEF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m_globalRunesInfo.MHP = (1 / (1 + Slider_ATT.Value) + Slider_LEF.Value);
            m_globalRunesInfo.ATT = (1 + Slider_ATT.Value);
            m_globalRunesInfo.DEF = ((1 / (1 + Slider_ATT.Value)) * (1 + Slider_DEF.Value) - (Slider_LEF.Value * 1.6));
            m_globalRunesInfo.RES = ((1 / (1 + Slider_ATT.Value)) * (1 - Slider_DEF.Value) - (Slider_LEF.Value * 1.6));
        }
    }

    
}
