using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IMainPlug;

namespace TestProgect
{
    public class MainInfomation : ITranslaterInterface
    {
        private DataSet mainData = new DataSet();
        public DataSet MainData
        {
            get { return mainData; }
            set { mainData = value; }
        }


        public string teststring = "测试反射数据";
        public string testReflection()
        {
            return "测试方法\r\n ";
        }

        //以下为接口方法实现

        public bool FileFilter(ref string extend)
        {
            return true;
        }

        public bool FileImport(string path, out DataTable importdata)
        {
            importdata = new DataTable();
            return true;
        }

        public bool FileExport(string path, DataTable outputdata)
        {
            return true;
        }

        public bool DataNeeded()
        {
            return false;
        }

        public DataSet GetDataSet()
        {
            return mainData;
        }

        public string PlugInfo()
        {
            string temp = string.Format("调用{0}插件成功\r\n", ToString());
            temp += "接口名:ITranslaterInterface";
            return temp;
        }
    }
}
