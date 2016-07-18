using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;
using System.IO;

namespace GameTranslaterUI
{
    public partial class WindowLogin : Window
    {
        /// <summary>
        /// 设置文件监视器
        /// </summary>
        public void SettingPlugsWatcher()
        {
            FileSystemWatcher plugChangeWatcher = new FileSystemWatcher();
            plugChangeWatcher.Path = m_appStartupPath + @"\Plugs\";
            plugChangeWatcher.Filter = "*.dll";
            plugChangeWatcher.EnableRaisingEvents = true;

            plugChangeWatcher.Changed += plugChangeWatcher_Changed;
        }
    }
}
