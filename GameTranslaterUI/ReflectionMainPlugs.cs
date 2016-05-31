using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace GameTranslaterUI
{
    static class ReflectionMainPlugs
    {
        public static Assembly m_plugAssembly = null;
        public static List<string> m_plugList = new List<string>();

        /// <summary>
        /// 检测并读取插件名
        /// </summary>
        static public List<string> checkPlugFiles(string path)
        {
            path = path + @"\Plugs\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            m_plugList.Clear();

            foreach (var item in Directory.GetFiles(path))
            {
                if (item.EndsWith(".dll"))
                {
                    string temp = item.Replace(path, "");
                    m_plugList.Add(temp);
                }
            }
            return m_plugList;
        }


        static public bool LoadAssembly(string path,string name)
        {
           
            Type type;
            Object obj;

 

            Object any = new Object();

            try
            {
                m_plugAssembly = Assembly.LoadFile(path + name);
            }
            catch (Exception)
            {
                return false;
            }
            
            //type = m_plugAssembly.GetType();

            Type[] ts = m_plugAssembly.GetTypes();

            MethodInfo method = ts[0].GetMethod("add");

            //int count = (int)method.Invoke(null, null);

            return true;
        }
    }
}
