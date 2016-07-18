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

            //状态绑定
            bindingState();
            //符文绑定
            bindingRunes();
        }
    }
}

