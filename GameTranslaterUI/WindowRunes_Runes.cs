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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RuneDesign;

namespace GameTranslaterUI
{
    public partial class WindowRunes : Window
    {
        private InfoRunes m_globalRunesInfo = null;

        public void BindingRunes()
        {
            TextBox_MHP.SetBinding(TextBox.TextProperty, new Binding("MHP") { Source = m_globalRunesInfo });
            TextBox_ATT.SetBinding(TextBox.TextProperty, new Binding("ATT") { Source = m_globalRunesInfo });
            TextBox_DEF.SetBinding(TextBox.TextProperty, new Binding("DEF") { Source = m_globalRunesInfo });
            TextBox_RES.SetBinding(TextBox.TextProperty, new Binding("RES") { Source = m_globalRunesInfo });

            slider_LEF.Value = 0;
            slider_ATT.Value = 0;
            slider_DEF.Value = 0;
        }

        private void SettingRunesData()
        {
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
                InfoReturn[i] = basic.Calculate(state, out min, i, TextBox_MHP.Text, TextBox_ATT.Text, TextBox_DEF.Text, TextBox_RES.Text);
                deviation = deviation + min;
                basic = null;
            }
            deviation = deviation / 16;

            InfoRunes infoRunesFormat = new InfoRunes();

            textBox.Text = "";

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
                textBox.Text = textBox.Text + infoRunesFormat.ReturnGroups_Type1(InfoReturn);
            }

            //以品质名符文组方式导出
            if ((bool)radioButton_Format_3.IsChecked)
            {
                textBox.Text = textBox.Text + infoRunesFormat.ReturnGroups_Type2(InfoReturn);
            }

            TextBox_Deviation.Text = deviation.ToString();
        }

        
    }
}
