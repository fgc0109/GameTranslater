using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Data;
using System.Globalization;

namespace GameTranslaterUI.Converter
{
    class ConverterImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string severity = value as string;
            if (severity != null)
            {
                if (severity == "SQLite")
                    return ResourceHelper.GetBitmapImage("database_sqlite.png");
                else if (severity == "MySQL")
                    return ResourceHelper.GetBitmapImage("database_mysql.png");
                else
                    return null;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string databaseType = "";
        public string DatabaseType
        {
            get { return databaseType; }
            set { databaseType = value; }
        }
    }
}
