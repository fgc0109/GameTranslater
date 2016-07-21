using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;

namespace GameTranslaterUI
{
    /// <summary>
    /// 绑定窗口控件数据
    /// </summary>
    public partial class WindowTrans : Window
    {
        public void BindingState()
        {
            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("MainDataTable") { Source = mGlobalBasicInfo });
        }
    }
}
