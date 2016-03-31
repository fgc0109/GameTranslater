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
        Binding bind = new Binding();

        public void BindingState()
        {
            BasicInfo basic = new BasicInfo();
            Binding bind = new Binding("State");
            //bind.Path = new PropertyPath("State");
            bind.Source = basic;

            basic.State = true;
            textBox0.SetBinding(TextBox.TextProperty, bind);

            image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));
        }
    }
}
