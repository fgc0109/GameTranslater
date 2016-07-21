using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;
using System.IO;
using System;

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
            plugChangeWatcher.Path = mAppStartupPath + @"\Plugs\";
            plugChangeWatcher.Filter = "*.dll";
            plugChangeWatcher.EnableRaisingEvents = true;

            plugChangeWatcher.Changed += plugChangeWatcher_Changed;

            Dispatcher.Invoke(new Action(() => { mGlobalBasicInfo.InfoPlugList = ReflectionMainPlugs.CheckPlugFiles(mAppStartupPath, "ITranslaterInterface"); }));
            Dispatcher.Invoke(new Action(() => { listView_Plugs.SelectedIndex = 0; }));
        }
    }
}
