using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IMainPlug
{
    /// <summary>
    /// 插件主接口
    /// </summary>
    public interface ITranslaterInterface
    {
        /// <summary>
        /// 程序主入口点,在主程序中首先调用
        /// </summary>
        /// <returns></returns>
        bool StartPoint(string path,string name);

        /// <summary>
        /// 界面的打开是否需要强制加载数据
        /// </summary>
        /// <returns></returns>
        bool DataNeeded();

        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <returns>已经读取的数据集</returns>
        DataSet GetDataSet();

        /// <summary>
        /// 获取插件基本信息
        /// </summary>
        /// <returns>信息字符串</returns>
        string PlugInfo();
    }
}
