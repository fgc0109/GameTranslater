using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTranslaterUI
{
    public class RuneCalculate
    {
        public double[] m_MHP = new double[10] { 1, 0, 0, 0, 0.5, 0, 0, 0.5, 0.5, 0 };
        public double[] m_ATT = new double[10] { 0, 1, 0, 0, 0.5, 0.5, 0, 0, 0, 0.5 };
        public double[] m_DEF = new double[10] { 0, 0, 1, 0, 0, 0.5, 0.5, 0.5, 0, 0 };
        public double[] m_RES = new double[10] { 0, 0, 0, 1, 0, 0, 0.5, 0, 0.5, 0.5 };

        private double[] m_quality = new double[6] { 1, 1.5, 2.25, 3.25, 4.5, 6 };

        private int[,] m_LVL = new int[16, 6]
        {
            {4, 1, 0, 0, 0, 0},
            {0, 5, 0, 0, 0, 0},
            {0, 4, 1, 0, 0, 0},
            {0, 2, 3, 0, 0, 0},
            {0, 0, 5, 0, 0, 0},
            {0, 0, 4, 1, 0, 0},
            {0, 0, 2, 3, 0, 0},
            {0, 0, 1, 4, 0, 0},
            {0, 0, 0, 5, 0, 0},
            {0, 0, 0, 4, 1, 0},
            {0, 0, 0, 3, 2, 0},
            {0, 0, 0, 2, 3, 0},
            {0, 0, 0, 1, 4, 0},
            {0, 0, 0, 0, 5, 0},
            {0, 0, 0, 0, 4, 1},
            {0, 0, 0, 0, 0, 5},
          };

        List<int[]> lst_Combination_A = null;
        List<int[]> lst_Permutation = null;

        public string Calculate(out bool state,int lv, string strMHP, string strATT, string strDEF, string strRES)
        {
            double MHP, ATT, DEF, RES;

            state = false;

            string strRes = "";
            RuneCalculate runeInfo = new RuneCalculate();

            string[] a = new string[10];
            //取组合C10,5
            int[] arrayCom = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            lst_Combination_A = PermutationAndCombination<int>.GetCombination(arrayCom, 5);

            for (int i = 0; i < lst_Combination_A.Count; i++)
            {
                MHP = ATT = DEF = RES = 0;

                //取排列A5,X
                int[] arrayPar = new int[] { 0, 1, 2, 3, 4 };


                int flag_quality_1 = 0;
                int flag_quality_2 = 0;

                for (int quality = 0; quality < 6; quality++)
                {
                    if(m_LVL[lv, quality]!=0)
                    {
                        lst_Permutation = PermutationAndCombination<int>.GetPermutation(arrayPar, m_LVL[lv, quality]);
                        flag_quality_1 = quality;
                        if(m_LVL[lv, quality] != 5)
                        {
                            flag_quality_2 = quality + 1;
                        }
                        else
                        {
                            flag_quality_2 = 0;
                        }
                        quality = 6;
                    }
                    
                }


               

                for (int j = 0;  j< lst_Combination_A.Count; j++)
                {

                }


                

                foreach (var item in lst_Combination_A[i])
                {
                    a[lst_Combination_A[i][item]] = "bai";
                }
                foreach (var item in lst_Permutation[i])
                {
                    a[lst_Combination_A[i][item]] = "lv";
                }



                double[] result = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                foreach (int item in result)
                    result[item] = 0.0;


                foreach (var item in lst_Combination_A[i])
                {
                    MHP = MHP + runeInfo.m_MHP[item];
                    ATT = ATT + runeInfo.m_ATT[item];
                    DEF = DEF + runeInfo.m_DEF[item];
                    RES = RES + runeInfo.m_RES[item];
                }


                if (double.Parse(strMHP) + double.Parse(strATT) + double.Parse(strDEF) + double.Parse(strRES) != 5)
                {
                    state = false;
                }
                else
                {
                    state = true;
                }
                if (MHP == double.Parse(strMHP) &&
                    ATT == double.Parse(strATT) &&
                    DEF == double.Parse(strDEF) &&
                    RES == double.Parse(strRES))
                {
                    foreach (var item in lst_Combination_A[i])
                    {
                        result[item] = 1;
                    }
                    foreach (int item in result)
                    {
                        strRes = strRes + item.ToString() + "\r\n";
                    }
                    strRes = strRes + "\r\n";
                }
            }

            return strRes;
        }
    }

}
