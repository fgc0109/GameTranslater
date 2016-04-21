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
            bool[] state =new bool[10];
            double min;
            double min2 = 0;



            state[0] = (bool)checkBox_Copy0.IsChecked;
            state[1] = (bool)checkBox_Copy1.IsChecked;
            state[2] = (bool)checkBox_Copy2.IsChecked;
            state[3] = (bool)checkBox_Copy3.IsChecked;
            state[4] = (bool)checkBox_Copy4.IsChecked;
            state[5] = (bool)checkBox_Copy5.IsChecked;
            state[6] = (bool)checkBox_Copy6.IsChecked;
            state[7] = (bool)checkBox_Copy7.IsChecked;
            state[8] = (bool)checkBox_Copy8.IsChecked;
            state[9] = (bool)checkBox_Copy9.IsChecked;

            double[][] InfoReturn = new double[16][];
            for (int i = 0; i < 16; i++)
            {
                RuneCalculate basic = new RuneCalculate();
                InfoReturn[i] = basic.Calculate( state, out min, i, TextBox_MHP.Text, TextBox_ATT.Text, TextBox_DEF.Text, TextBox_RES.Text);
                min2 = min2 + min;
                basic = null;
            }
            min2 = min2 / 16;

            InfoRunes newlabel = new InfoRunes();
            StringBuilder outputInfoBuilder = new StringBuilder(4096);

            //以权重值方式导出
            if ((bool)radioButton_Copy0.IsChecked)
            {
                for (int j = 0; j < 14; j++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        outputInfoBuilder.Append(InfoReturn[i][j]);
                        outputInfoBuilder.Append("\t");
                    }
                    if (j == 9)
                        outputInfoBuilder.Append('-', 145);
                    outputInfoBuilder.AppendLine();
                }
                outputInfoBuilder.Append('=', 85);
                outputInfoBuilder.AppendLine();

                textBox.Text = textBox.Text + outputInfoBuilder;
            }

            //以品质名方式导出
            if ((bool)radioButton_Copy1.IsChecked)
            {
                for (int j = 0; j < 14; j++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        outputInfoBuilder.Append(newlabel.ReturnLabel(InfoReturn[i][j]));
                        outputInfoBuilder.Append("\t");
                    }
                    if (j == 9)
                        outputInfoBuilder.Append('-', 145);
                    outputInfoBuilder.AppendLine();
                }
                outputInfoBuilder.Append('=', 85);
                outputInfoBuilder.AppendLine();

                textBox.Text = textBox.Text + outputInfoBuilder;
            }

            //以符文组方式导出
            if ((bool)radioButton_Copy2.IsChecked)
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        outputInfoBuilder.Append(newlabel.ReturnID(InfoReturn[i][j], j, "\t"));
                    }
                    outputInfoBuilder.AppendLine();
                }
                textBox.Text = textBox.Text + outputInfoBuilder;
            }

            //foreach (var item in basic.Calculate(out state, out min, int.Parse(TextBox_Level.Text) - 1, TextBox_MHP.Text, TextBox_ATT.Text, TextBox_DEF.Text, TextBox_RES.Text))
            //{
            //    textBox.Text = textBox.Text + item.ToString() + "\r\n";
            //}
            //textBox.Text = textBox.Text + "\r\n";


            //textBox.Text = InfoReturn[0][0].ToString() + InfoReturn[1][0].ToString();

            TextBox_Deviation.Text = min2.ToString();

            //List<string[]> info = new List<string[]>();

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
