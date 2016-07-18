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
using IMainPlug;

namespace GameTranslaterUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WindowRunes : Window
    {
        public WindowRunes(params object[] paraValues)
        {
            InitializeComponent();

            BindingRunes();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SettingRunesData();
        }

        private void slider_LEF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m_globalRunesInfo.MHP = (1 / (1 + slider_ATT.Value) + slider_LEF.Value);
            m_globalRunesInfo.ATT = (1 + slider_ATT.Value);
            m_globalRunesInfo.DEF = ((1 / (1 + slider_ATT.Value)) * (1 + slider_DEF.Value) - (slider_LEF.Value * 1.6));
            m_globalRunesInfo.RES = ((1 / (1 + slider_ATT.Value)) * (1 - slider_DEF.Value) - (slider_LEF.Value * 1.6));
        }

        private void slider_ATT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m_globalRunesInfo.MHP = (1 / (1 + slider_ATT.Value) + slider_LEF.Value);
            m_globalRunesInfo.ATT = (1 + slider_ATT.Value);
            m_globalRunesInfo.DEF = ((1 / (1 + slider_ATT.Value)) * (1 + slider_DEF.Value) - (slider_LEF.Value * 1.6));
            m_globalRunesInfo.RES = ((1 / (1 + slider_ATT.Value)) * (1 - slider_DEF.Value) - (slider_LEF.Value * 1.6));
        }

        private void slider_DEF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            m_globalRunesInfo.MHP = (1 / (1 + slider_ATT.Value) + slider_LEF.Value);
            m_globalRunesInfo.ATT = (1 + slider_ATT.Value);
            m_globalRunesInfo.DEF = ((1 / (1 + slider_ATT.Value)) * (1 + slider_DEF.Value) - (slider_LEF.Value * 1.6));
            m_globalRunesInfo.RES = ((1 / (1 + slider_ATT.Value)) * (1 - slider_DEF.Value) - (slider_LEF.Value * 1.6));
        }
    }
}

