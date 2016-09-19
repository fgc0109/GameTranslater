using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IMainPlug;

namespace ThisWarOfMine
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

        public bool FileFilter(ref string filter)
        {
            filter = "IDX - File(*.idx) | *.idx| 所有文件(*.*) | *.*";
            return true;
        }

        public bool FileImport(string path, out DataTable importdata)
        {
            string error = String.Empty;
            FilesDecoding.FileLoad(path, out error);
            FilesDecoding.DataUnpacking(out error);
            FilesDecoding.DataUncompress();

            importdata = new DataTable();
            return true;
        }

        public bool FileExport(string path, DataTable outputdata)
        {
            return true;
        }

        public bool DataNeeded()
        {
            return true;
        }

        public DataSet GetDataSet()
        {
            return mainData;
        }

        public string PlugInfo()
        {
            string temp = "调用插件成功\r\n";
            temp += "接口名:ITranslaterInterface";
            return temp;
        }
    }
}
