using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;

namespace DataManager
{
    class MainData
    {
        static DataSet m_mainDataSet = null;
        static List<DataTable> m_mainDataTable = null;
        
        public void DropDatas()
        {
            
        }

        public void Initialize()
        {

        }

        internal void GetDatas()
        {
            for (int i = 0; i < m_mainDataSet.Tables.Count; i++)
            {
                m_mainDataTable.Add(m_mainDataSet.Tables[i]);
            }
            
        }
    }
    public static class BasicInfo
    {
        public static bool m_stateDatabase = false;
        public static bool m_stateDataSets = false;
        public static string m_infoWindow = "";
        public static Bitmap m_icon = null;

    }
}
