using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataHelper;

namespace GameTranslaterUI
{
    /// <summary>
    /// WindowLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLogin : Window
    {
        WindowMain basicWindow = null;
        WindowTrans transWindow = null;


        public WindowLogin()
        {
            InitializeComponent();
            InitializingDefult();
        }

        private void InitializingDefult()
        {
            TextBox_Addr.Text = "localhost";
            TextBox_Port.Text = "3306 ";
            TextBox_User.Text = "root";
            TextBox_Pass.Text = "123456";
            TextBox_Base.Text = "refdata";
            TextBox_Table.Text = "border";
        }

        private void button_LoginIn_Click(object sender, RoutedEventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    try
                    {
                        MySqlHelper.OpenMySql(TextBox_Addr.Text, TextBox_Port.Text, TextBox_User.Text, TextBox_Pass.Text, TextBox_Base.Text);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (MySqlHelper.Connection.State != System.Data.ConnectionState.Open)
                        {
                            TextBox_DebugInfo.Text = "数据库未连接";
                        }
                        else
                        {
                            //basicWindow = new WindowMain();
                            //basicWindow.Show();
                            //Hide();


                            transWindow = new WindowTrans(MySqlHelper.ExecuteDataSet("select * from {0};", TextBox_Table.Text));
                            transWindow.Show();
                            //Close();

                        }
                    }


                    break;

                default:

                    break;
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TextBox_DebugInfo.Text = tabControl.SelectedIndex.ToString();


        }
    }
}
