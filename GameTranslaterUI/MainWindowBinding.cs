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
            BasicInfo aaa = new BasicInfo();
            Binding bind = new Binding();
            bind.Source = aaa;

            aaa.State = true;


            bind.Path = new PropertyPath("State");
          
            textBox.SetBinding(TextBox.TextProperty, bind);


          

            image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));

        }
    }
}
