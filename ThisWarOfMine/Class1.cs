using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IMainPlug;

namespace ThisWarOfMine
{
    public class Class1:ITranslaterInterface
    {
        public string teststring = "test 测试反射用数据";
        public string returnstring()
        {
            return "测试反射用方法\r\n 切记要重新生成插件";
        }

        public DataSet languageDataSet()
        {
            return new DataSet();
        }



        public string plugInfomation()
        {
            return "使用接口方法";
        }
    }
}
