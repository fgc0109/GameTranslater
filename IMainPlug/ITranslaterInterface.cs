﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IMainPlug
{
    /// <summary>
    /// 主接口
    /// </summary>
    public interface ITranslaterInterface
    {
        /// <summary>
        /// 程序主入口点,在主程序中首先调用
        /// </summary>
        /// <returns></returns>
        bool mainStartPoint(string path,string name);

        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <returns>已经读取的数据集</returns>
        DataSet languageDataSet();

        /// <summary>
        /// 获取插件基本信息
        /// </summary>
        /// <returns>信息字符串</returns>
        string plugInfomation();
    }
}
