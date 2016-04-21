using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GameTranslaterUI
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

        public string ReturnLabel(double quality)
        {
            if (quality == 1)
                return "白";
            else if (quality == 1.5)
                return "绿";
            else if (quality == 2.25)
                return "蓝";
            else if (quality == 3.25)
                return "紫";
            else if (quality == 4.5)
                return "橙";
            else if (quality == 6)
                return "红";
            else
                return "";
        }

        public string ReturnID(double quality, int type, string split)
        {
            type = type + 1;
            string temp = "";
            if (type == 10)
                temp = type.ToString();
            else if (type == 0)
                temp = "";
            else
                temp = "0" + type.ToString() + split;

            if (quality == 1)
                return "10" + temp;
            else if (quality == 1.5)
                return "20" + temp;
            else if (quality == 2.25)
                return "30" + temp;
            else if (quality == 3.25)
                return "40" + temp;
            else if (quality == 4.5)
                return "50" + temp;
            else if (quality == 6)
                return "60" + temp;
            else
                return "";
        }


        public string ReturnSeparator(int length, char sp)
        {
            string temp = "";
            temp = temp + "\r\n";
            for (int i = 0; i < length; i++)
            {
                temp = temp + sp;
            }
            temp = temp + "\r\n";

            return temp;
        }
    }
}
