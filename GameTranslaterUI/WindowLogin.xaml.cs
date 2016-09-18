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
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using DataHelper;
using IMainPlug;

namespace GameTranslaterUI
{
    /// <summary>
    /// WindowLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLogin : Window
    {
        public readonly string mAppStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        private BasicInfomation mGlobalBasicInfo = null;
        private WindowTrans mTransWindow = null;

        public WindowLogin()
        {
            InitializeComponent();

            mGlobalBasicInfo = new BasicInfomation();
            SettingPlugsWatcher();

            mTransWindow = new WindowTrans(null, mGlobalBasicInfo.InfoPlugList);
            mTransWindow.Show();

            Close();
        }

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
        }

        #region ==========CallBack

        private void plugChangeWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { mGlobalBasicInfo.InfoPlugList = ReflectionMainPlugs.CheckPlugFiles(mAppStartupPath, "ITranslaterInterface"); }));
        }

        #endregion

    }
}
