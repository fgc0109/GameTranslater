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
            //aaa.Name = textBox.Text;
            aaa.State = false;


            bind.Path = new PropertyPath("State");
            //textBox.SetBinding(TextBox.TextProperty, bind);
            textBox.SetBinding(TextBox.TextProperty, bind);


            List<Binding> m_stateBinding = new List<Binding>();

            //Bitmap bbb=new Bitmap()
            InitializeComponent();
            Uri imageURI = new Uri(@"pack://application:,,,/Resource/Image/20090102191236877.gif", UriKind.Absolute);
            this.image.Source = new BitmapImage(imageURI);

            m_stateBinding[0] = new Binding("Source");
            m_stateBinding[0].Source = "Resources/state_readt.png";
           
        }
    }
}
