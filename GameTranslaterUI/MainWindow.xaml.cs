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

            double MHP = 0;
            double ATT = 0;
            double DEF = 0;
            double RES = 0;
            RuneInfo runeInfo = new RuneInfo();
            textBox.Text = "";

            int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int[]> lst_Combination = PermutationAndCombination<int>.GetCombination(array, 5);

            for (int i = 0; i < lst_Combination.Count; i++)
            {
                MHP = 0;
                ATT = 0;
                DEF = 0;
                RES = 0;
                double[] result = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                foreach (int item in result)
                    result[item] = 0.0;
                foreach (var item in lst_Combination[i])
                {
                    MHP = MHP + runeInfo.m_MHP[item];
                    ATT = ATT + runeInfo.m_ATT[item];
                    DEF = DEF + runeInfo.m_DEF[item];
                    RES = RES + runeInfo.m_RES[item];
                }
                if (double.Parse(TextBox_Life.Text) + double.Parse(TextBox_Attack.Text) + double.Parse(TextBox_Defence.Text) + double.Parse(TextBox_Magic.Text) != 5)
                {
                    image.Visibility = Visibility.Visible;
                    image1.Visibility = Visibility.Hidden;
                }
                else
                {
                    image.Visibility = Visibility.Hidden;
                    image1.Visibility = Visibility.Visible;

                }
                if (MHP == double.Parse(TextBox_Life.Text) &&
                    ATT == double.Parse(TextBox_Attack.Text) &&
                    DEF == double.Parse(TextBox_Defence.Text) &&
                    RES == double.Parse(TextBox_Magic.Text))
                {
                    foreach (var item in lst_Combination[i])
                    {
                        result[item] = 1;
                    }
                    foreach (int item in result)
                    {
                        textBox.Text = textBox.Text + item.ToString() + "\r\n";
                    }
                    textBox.Text = textBox.Text + "\r\n";
                }
            }
        }
    }
}
