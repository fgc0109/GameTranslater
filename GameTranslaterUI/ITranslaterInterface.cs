using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameTranslaterUI
{
    /// <summary>
    /// 主接口
    /// </summary>
    interface ITranslaterInterface
    {
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
