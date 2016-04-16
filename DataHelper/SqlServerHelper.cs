using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataHelper
{
    public class SqlServerHelper
    {
        static public SqlConnection m_dbConnection = null;
        static public SqlDataAdapter m_dbDataAdapter = null;
    }
}
