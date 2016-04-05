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
        BasicInfo m_globalBasicInfo = null;

        public void BindingState()
        {
            m_globalBasicInfo = new BasicInfo();

            stateDatabase_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDB") { Source = m_globalBasicInfo });
            stateDataSets_TextBox.SetBinding(TextBox.TextProperty, new Binding("InfoDS") { Source = m_globalBasicInfo });

            m_globalBasicInfo.InfoDB = "12121212";
            m_globalBasicInfo.InfoDS = "131313131";

            //image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));
        }
    }

    internal class SetGlobal:SetGlobalData
    {
        public void SetStateDB(bool state)
        {

        }
        public void SetStateDS(bool state)
        {

        }
        public void SetInfoDB(string info)
        {

        }
        public void SetInfoDS(string info)
        {

        }
    }
}
