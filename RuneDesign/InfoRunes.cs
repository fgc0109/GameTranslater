using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RuneDesign
{
    public class InfoRunes : INotifyPropertyChanged
    {
        private double m_MHP = 0;
        private double m_ATT = 0;
        private double m_DEF = 0;
        private double m_RES = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public double MHP
        {
            get { return m_MHP; }
            set
            {
                m_MHP = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MHP"));
                }
            }
        }

        public double ATT
        {
            get { return m_ATT; }
            set
            {
                m_ATT = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ATT"));
                }
            }
        }

        public double DEF
        {
            get { return m_DEF; }
            set
            {
                m_DEF = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DEF"));
                }
            }
        }

        public double RES
        {
            get { return m_RES; }
            set
            {
                m_RES = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RES"));
                }
            }
        }

        public string  ReturnWeights(double[][] infoReturn)
        {
            StringBuilder outputInfoBuilder = new StringBuilder(4096);
            for (int j = 0; j < 14; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    outputInfoBuilder.Append(infoReturn[i][j]);
                    outputInfoBuilder.Append("\t");
                }
                if (j == 9)
                    outputInfoBuilder.Append('-', 145);
                outputInfoBuilder.AppendLine();
            }
            outputInfoBuilder.Append('=', 85);
            outputInfoBuilder.AppendLine();

            return outputInfoBuilder.ToString();
        }

        public string ReturnQuality(double[][] infoReturn)
        {
            StringBuilder outputInfoBuilder = new StringBuilder(4096);
            for (int j = 0; j < 14; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    outputInfoBuilder.Append(QualityInfo(infoReturn[i][j], string.Empty));
                    outputInfoBuilder.Append("\t");
                }
                if (j == 9)
                    outputInfoBuilder.Append('-', 145);
                outputInfoBuilder.AppendLine();
            }
            outputInfoBuilder.Append('=', 85);
            outputInfoBuilder.AppendLine();

            return outputInfoBuilder.ToString();
        }

        public string ReturnGroups_Type1(double[][] infoReturn)
        {
            StringBuilder outputInfoBuilder = new StringBuilder(4096);
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    outputInfoBuilder.Append(GroupsInfo(infoReturn[i][j], j, "\t"));
                }
                outputInfoBuilder.AppendLine();
            }

            return outputInfoBuilder.ToString();
        }

        public string ReturnGroups_Type2(double[][] infoReturn)
        {
            StringBuilder outputInfoBuilder = new StringBuilder(4096);
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    outputInfoBuilder.Append(QualityInfo(infoReturn[i][j],"\t"));
                }
                outputInfoBuilder.AppendLine();
            }

            return outputInfoBuilder.ToString();
        }

        public string QualityInfo(double quality, string split)
        {
            if (quality == RuneCalculate.m_qualityRunes[0])
                return "白" + split;
            else if (quality == RuneCalculate.m_qualityRunes[1])
                return "绿" + split;
            else if (quality == RuneCalculate.m_qualityRunes[2])
                return "蓝" + split;
            else if (quality == RuneCalculate.m_qualityRunes[3])
                return "紫" + split;
            else if (quality == RuneCalculate.m_qualityRunes[4])
                return "橙" + split;
            else if (quality == RuneCalculate.m_qualityRunes[5])
                return "红" + split;
            else
                return "" + split;
        }

        public string GroupsInfo(double quality, int type, string split)
        {
            type = type + 1;
            string temp = "";
            if (type == 10)
                temp = type.ToString();
            else if (type == 0)
                temp = "";
            else
                temp = "0" + type.ToString() + split;

            if (quality == RuneCalculate.m_qualityRunes[0])
                return "10" + temp;
            else if (quality == RuneCalculate.m_qualityRunes[1])
                return "20" + temp;
            else if (quality == RuneCalculate.m_qualityRunes[2])
                return "30" + temp;
            else if (quality == RuneCalculate.m_qualityRunes[3])
                return "40" + temp;
            else if (quality == RuneCalculate.m_qualityRunes[4])
                return "50" + temp;
            else if (quality == RuneCalculate.m_qualityRunes[5])
                return "60" + temp;
            else
                return "";
        }
    }
}
