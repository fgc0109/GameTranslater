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

        static List<bool> m_mainState = null;
        
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
    public class BasicInfo
    {
        private bool m_state = false;
        private Bitmap m_icon = null;


        public bool State
        {
            get { return m_state; }
            set
            {
                m_state = value;
            }
        }

        public Bitmap Icon
        {
            get { return m_icon; }
            set
            {
                m_icon = value;
            }
        }
    }
}
