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
        private BasicInfomation m_globalBasicInfo = null;

        /// <summary>
        /// 绑定全局状态数据
        /// </summary>
        public void BindingState()
        {
            m_globalBasicInfo = new BasicInfomation();
            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("MainDataTable") { Source = m_globalBasicInfo });           
        }   
    }
}
