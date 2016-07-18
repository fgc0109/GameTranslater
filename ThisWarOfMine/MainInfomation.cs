﻿using System;
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

        public bool StartPoint(string path, string name)
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
