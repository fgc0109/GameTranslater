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


        static public string LoadAssembly(string path, string name)
        {
            //
            Type type = null;
            Type[] types = null;

            //类型的完全限定名
            string fullName = name.Replace(".dll", ".") + "Class1";

            //使用绝对路径读取程序集文件
            try
            {
                m_plugAssembly = Assembly.LoadFile(path + name);
            }
            catch (Exception)
            {
                return "";
            }

            //获取程序集中的类
            type = m_plugAssembly.GetType(fullName, true);
            types = m_plugAssembly.GetTypes();



            //获取方法
            MethodInfo method = type.GetMethod("returnstring");

            //对于实例方法需要创建对象实例
            if (method != null)
            {
                Object obj = m_plugAssembly.CreateInstance(fullName, true);
                Object[] parametors = new Object[] { };
                string count = (string)method.Invoke(obj, parametors);

                return count;
            }

            else
            {
                string count = "没有找到方法";
                return count;
            }

            //对于静态方法
            //if (method != null)
            //{
            //    Object[] parametors = new Object[] { };
            //    string count = (string)method.Invoke(null, parametors);

            //    return count;
            //}
        }
    }
}
