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
using System.Windows.Shapes;

namespace GameTranslaterUI
{
    /// <summary>
    /// WindowTrans.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTrans : Window
    {
        private BasicInfomation m_globalBasicInfo = null;

        public WindowTrans(params object[] paraValues)
        {
            InitializeComponent();
            m_globalBasicInfo = new BasicInfomation();

            BindingState();

            m_globalBasicInfo.MainDataTable = paraValues[1] as DataTable;
        }

        private void button_Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
