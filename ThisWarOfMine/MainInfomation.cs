using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IMainPlug;

namespace ThisWarOfMine
{
    public class MainInfomation:ITranslaterInterface
    {
        public string teststring = "test 测试反射用数据";
        public string testReflection()
        {
            return "测试反射用方法\r\n 切记要重新生成插件";
        }

        //以下为接口方法实现

        public bool mainStartPoint(string path, string name)
        {
            return true;
        }

        public DataSet languageDataSet()
        {
            return new DataSet();
        }

        public string plugInfomation()
        {
            string temp = "使用实例接口方法调用插件成功\r\n";
            temp += "接口名:ITranslaterInterface";
            return temp;
        }
    }
}
