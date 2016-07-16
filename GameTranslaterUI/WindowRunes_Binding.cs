using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataHelper;
using RuneDesign;

namespace GameTranslaterUI
{
    public partial class WindowRunes : Window
    {
        private BasicInfomation m_globalBasicInfo = null;
        private DataSet m_mainDataSet = null;
        private List<DataTable> m_mainDataTable = null;

        public void bindingState()
        {
            m_globalRunesInfo = new InfoRunes();
        }

        internal void getDatas()
        {
            for (int i = 0; i < m_mainDataSet.Tables.Count; i++)
            {
                m_mainDataTable.Add(m_mainDataSet.Tables[i]);
            }
        }
    }
}
