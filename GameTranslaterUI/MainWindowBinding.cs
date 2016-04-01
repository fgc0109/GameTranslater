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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using DataManager;

namespace GameTranslaterUI
{
    public partial class MainWindow : Window
    {
        public void BindingState()
        {
            BasicInfo.m_stateDatabase = true;
            textBox0.SetBinding(TextBox.TextProperty, new Binding(".") { Source = BasicInfo.m_stateDatabase });
            textBox1.SetBinding(TextBox.TextProperty, new Binding(".") { Source = BasicInfo.m_stateDataSets });

            //button.SetBinding(Button.IsPressedProperty, new Binding(".") { Source = BasicInfo.m_stateDatabase });
            image1.SetBinding(System.Windows.Controls.Image.VisibilityProperty, new Binding(".") { Source = BasicInfo.m_stateDatabase });

            image1.SetBinding(System.Windows.Controls.Image.VisibilityProperty, new Binding(".")
            {
                Source = BasicInfo.m_stateDatabase ? BasicInfo.m_stateDatabase : BasicInfo.m_stateDatabase
            });

            BasicInfo.m_stateDatabase = false;
            BasicInfo.m_stateDataSets = false;

            image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));
        }
    }
}
