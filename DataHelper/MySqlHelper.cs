using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataHelper
{
    public class MySqlHelper
    {
        static public MySqlConnection m_dbConnection = null;
        static public MySqlDataAdapter m_dbDataAdapter = null;
    }
}
