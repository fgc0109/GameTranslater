using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameTranslaterUI.Converter
{
    class ResourceHelper
    {
        static BitmapImage databaseMySQL = new BitmapImage(new Uri("pack://application:,,,/Resources/state_active.png"));

        public static BitmapImage GetBitmapImage(string name)
        {
            return new BitmapImage(new Uri("pack://application:,,,/Resources/" + name));
        }
    }
}
