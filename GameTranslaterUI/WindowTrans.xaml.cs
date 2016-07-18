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
        public WindowTrans(params object[] paraValues)
        {
            InitializeComponent();

            textBox.Text = "";
            foreach (var item in paraValues)
            {
                textBox.Text = textBox.Text + item.ToString() + "\n\r";
            }

            //dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { });
            DataTable temp = paraValues[1] as DataTable;
            if (temp != null)
            {
                dataGrid.ItemsSource = temp.AsDataView();
            }

        }
    }
}
