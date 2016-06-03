using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using IMainPlug;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace GameTranslaterUI
{
    static class ReflectionMainPlugs
    {
        public static Assembly m_plugAssembly = null;
        public static ObservableCollection<string> m_plugList = new ObservableCollection<string>();

        private static string m_interfaceName = String.Empty;

        public static string InterfaceName
        {
            get { return m_interfaceName; }
            set { m_interfaceName = value; }
        }

        /// <summary>
        /// 检测并读取插件名
        /// </summary>
        static public ObservableCollection<string> checkPlugFiles(string path)
        {
            path = path + @"\Plugs\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            m_plugList.Clear();
            m_plugAssembly = null;
            
            foreach (var item in Directory.GetFiles(path))
            {
                if (item.EndsWith(".dll"))
                {
                    string temp = item.Replace(path, "");

                    m_plugAssembly = Assembly.LoadFile(path + temp);
                    Type[] types = m_plugAssembly.GetTypes();

                    foreach (var items in types)
                        if (items.GetInterface(m_interfaceName) != null) m_plugList.Add(temp);
                }
            }
            return m_plugList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public string LoadAssembly(string path, string name)
        {

            Type type = null;
            Type[] types = null;
            m_plugAssembly = null;

            //类型的完全限定名
            string fullName = "";

            //使用绝对路径读取程序集文件
            m_plugAssembly = Assembly.LoadFile(path + name);

            //获取程序集中的类
            types = m_plugAssembly.GetTypes();

            //使用接口获取合适的程序集
            ITranslaterInterface loadedPlug = null;
            foreach (var item in types)
            {
                if (item.GetInterface("ITranslaterInterface") != null)
                {
                    loadedPlug = (ITranslaterInterface)Activator.CreateInstance(item);
                    fullName = item.FullName.ToString();
                }
            }
            if (loadedPlug != null)
            {
                return loadedPlug.plugInfomation();
            }

            type = m_plugAssembly.GetType(fullName, true);

            //对于不继承接口的反射,在运行时进行各种条件判断

            //获取方法
            MethodInfo method = type.GetMethod("testReflection");

            //对于实例方法需要创建对象实例
            if (method != null)
            {
                Object obj = m_plugAssembly.CreateInstance(fullName, true);
                Object[] parametors = new Object[] { };
                string str = (string)method.Invoke(obj, parametors);

                return str;
            }

            else
            {
                return "没有找到方法";
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
