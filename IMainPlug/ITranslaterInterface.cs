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
        /// 插件支持的文件类型筛选
        /// </summary>
        /// <returns></returns>
        bool FileFilter(ref string filter);

        /// <summary>
        /// 读取文件内容至数据表
        /// </summary>
        /// <returns></returns>
        bool FileImport(string path,ref DataTable importdata);

        /// <summary>
        /// 将当前数据表导出至文件
        /// </summary>
        /// <returns></returns>
        bool FileExport(string path,DataTable outputdata);

        /// <summary>
        /// 是否需要强制加载数据
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
