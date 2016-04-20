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

        private double[] m_qualityRunes = new double[6] { 1, 1.5, 2.25, 3.25, 4.5, 6 };
        private double[] m_qualityLevel = new double[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

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
        List<int[]> lst_Combination_B = null;
        List<int[]> lst_Permutation = null;

        //符文结果{品质参数*10,生命,攻击,物防,法防}
        List<double[]> lst_Appropriate = null;

        public string Calculate(out bool state,out double min, int lv, string strMHP, string strATT, string strDEF, string strRES)
        {
            double MHP, ATT, DEF, RES;

            state = false;
            min = 10;
            for(int i=0;i<16;i++)
            {
                for(int j=0;j<6;j++)
                {
                    m_qualityLevel[i] = m_qualityLevel[i] + m_LVL[i, j] * m_qualityRunes[j];
                }    
            }
            

            string strRes = "";
            RuneCalculate runeInfo = new RuneCalculate();

            lst_Appropriate = new List<double[]>();

            //取组合C10,5
            int[] arrayCom = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            lst_Combination_A = PermutationAndCombination<int>.GetCombination(arrayCom, 5);

            for (int i = 0; i < lst_Combination_A.Count; i++)
            {
                MHP = ATT = DEF = RES = 0;
                double[] result = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                //取排列A5,X
                //int[] arrayPar = new int[] { 0, 1, 2, 3, 4 };
                int[] arrayPar = lst_Combination_A[i];

                int flag_quality_1 = 0;
                int flag_quality_2 = 0;

                //获取等级相关的品质及对应数量组合数组
                #region
                for (int quality = 0; quality < 6; quality++)
                {
                    if (m_LVL[lv, quality] != 0)
                    {
                        lst_Combination_B = PermutationAndCombination<int>.GetCombination(arrayPar, m_LVL[lv, quality]);
                        flag_quality_1 = quality;
                        if (m_LVL[lv, quality] != 5)
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
                #endregion

                //品质有一种的时候
                #region
                if (flag_quality_2 == 0)
                {
                    //参数初始化
                    MHP = ATT = DEF = RES = 0;
                    for (int k = 0; k < 10; k++)
                        result[k] = 0.0;

                    //取出的5个符文编号
                    foreach (var item in lst_Combination_A[i])
                    {
                        //获取抽取结果及品质等级
                        result[item] = m_qualityRunes[flag_quality_1];
                        //抽取结果获取总属性加成
                        MHP = MHP + runeInfo.m_MHP[item] * m_qualityRunes[flag_quality_1];
                        ATT = ATT + runeInfo.m_ATT[item] * m_qualityRunes[flag_quality_1];
                        DEF = DEF + runeInfo.m_DEF[item] * m_qualityRunes[flag_quality_1];
                        RES = RES + runeInfo.m_RES[item] * m_qualityRunes[flag_quality_1];
                    }

                    //结果导出
                    double[] tempdata = new double[14];
                    for (int k = 0; k < 10; k++)
                    {

                        tempdata[k] = result[k];
                    }
                    tempdata[10] = MHP;
                    tempdata[11] = ATT;
                    tempdata[12] = DEF;
                    tempdata[13] = RES;
                    lst_Appropriate.Add(tempdata);
                }
                #endregion

                //品质有两种的时候
                #region
                else
                {
                    for (int j = 0; j < lst_Combination_B.Count; j++)
                    {
                        //参数初始化
                        MHP = ATT = DEF = RES = 0;
                        for (int k = 0; k < 10; k++)
                            result[k] = 0.0;

                        //取出5个符文编号
                        foreach (var item_A in lst_Combination_A[i])
                        {
                            //获取抽取结果及品质等级
                            result[item_A] = m_qualityRunes[flag_quality_2];
                        }
                        //记录5个符文编号
                        int[] item_test = new int[5];
                        for (int k = 0; k < 5; k++)
                        {
                            item_test[k] = lst_Combination_A[i][k];
                        }

                        //取出品质1对应数量并乘以品质1参数
                        foreach (var item_B in lst_Combination_B[j])
                        {
                            result[item_B] = m_qualityRunes[flag_quality_1];

                            MHP = MHP + runeInfo.m_MHP[item_B] * m_qualityRunes[flag_quality_1];
                            ATT = ATT + runeInfo.m_ATT[item_B] * m_qualityRunes[flag_quality_1];
                            DEF = DEF + runeInfo.m_DEF[item_B] * m_qualityRunes[flag_quality_1];
                            RES = RES + runeInfo.m_RES[item_B] * m_qualityRunes[flag_quality_1];

                            //找出已经使用的编号并置0
                            for (int k = 0; k < 5; k++)
                            {
                                if (item_test[k] == item_B)
                                {
                                    item_test[k] = 0;
                                }
                            }
                        }

                        //品质2对应数量并乘以品质2参数
                        for (int k = 0; k < 5; k++)
                        {
                            if (item_test[k] != 0)
                            {
                                MHP = MHP + runeInfo.m_MHP[item_test[k]] * m_qualityRunes[flag_quality_2];
                                ATT = ATT + runeInfo.m_ATT[item_test[k]] * m_qualityRunes[flag_quality_2];
                                DEF = DEF + runeInfo.m_DEF[item_test[k]] * m_qualityRunes[flag_quality_2];
                                RES = RES + runeInfo.m_RES[item_test[k]] * m_qualityRunes[flag_quality_2];
                            }
                        }

                        //结果导出
                        double[] tempdata = new double[14];
                        for (int k = 0; k < 10; k++)
                        {
                            
                            tempdata[k] = result[k];
                        }
                        tempdata[10] = MHP;
                        tempdata[11] = ATT;
                        tempdata[12] = DEF;
                        tempdata[13] = RES;
                        lst_Appropriate.Add(tempdata);
                    }
                    #endregion
                }
            }

            //匹配合适的权重
            int flag = 0;
            double temp = 0;

            double sum = double.Parse(strMHP) + double.Parse(strATT) + double.Parse(strDEF) + double.Parse(strRES);

            for (int i = 0; i < lst_Appropriate.Count; i++)
            {

                temp =
                    Math.Abs(lst_Appropriate[i][10] - double.Parse(strMHP) * m_qualityLevel[lv] / sum) +
                    Math.Abs(lst_Appropriate[i][11] - double.Parse(strATT) * m_qualityLevel[lv] / sum) +
                    Math.Abs(lst_Appropriate[i][12] - double.Parse(strDEF) * m_qualityLevel[lv] / sum) +
                    Math.Abs(lst_Appropriate[i][13] - double.Parse(strRES) * m_qualityLevel[lv] / sum);
                if (temp < min)
                {
                    min = temp;
                    flag = i;
                }
            }

            //返回匹配的符文组
            foreach (var item in lst_Appropriate[flag])
            {
                strRes = strRes + item.ToString() + "\r\n";
            }
            strRes = strRes + "\r\n";

            return strRes;
        }
    }
}

