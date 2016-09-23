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
        /// <summary>
        /// 检测插件并读取指定目录下实现指定接口的插件列表
        /// </summary>
        /// <param name="path">程序集绝对路径</param>
        /// <param name="interfaceName">接口名</param>
        /// <returns>可用插件列表</returns>
        static public ObservableCollection<string> CheckPlugFiles(string path, string interfaceName)
        {
            path = path + @"\Plugs\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            ObservableCollection<string> plugNameList = new ObservableCollection<string>();

            foreach (var item in Directory.GetFiles(path, "*.dll"))
            {
                string temp = item.Replace(path, "");
                Assembly plugAssembly = Assembly.LoadFile(path + temp);

                if (plugAssembly == null) continue;

                Type[] plugTypes = null;
                try
                {
                    plugTypes = plugAssembly.GetTypes();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    continue;
                }
                foreach (var items in plugTypes)
                    if (items.GetInterface(interfaceName) != null) plugNameList.Add(temp);
            }
            return plugNameList;
        }

        /// <summary>
        /// 加载实现ITranslaterInterface接口的程序集并创建对应实例
        /// </summary>
        /// <param name="path">程序集绝对路径</param>
        /// <param name="plugname">程序集名称</param>
        /// <returns>实现ITranslaterInterface接口的程序集实例装箱</returns>
        static public object LoadAssembly(string path, string plugname)
        {
            //使用绝对路径读取程序集文件
            Assembly plugAssembly = Assembly.LoadFile(path + plugname);

            //旧方法
            //Type[] types = plugAssembly.GetTypes();
            //Type[] types = plugAssembly.GetExportedTypes();
            Type[] types = (Type[])plugAssembly.ExportedTypes;

            //使用接口获取合适的程序集
            ITranslaterInterface loadedPlug = null;
            foreach (var item in types)
            {
                if (item.GetInterface("ITranslaterInterface") != null)
                {
                    loadedPlug = (ITranslaterInterface)Activator.CreateInstance(item);
                }
            }
            return loadedPlug;
        }

        /// <summary>
        /// 加载程序集并调用程序集方法
        /// </summary>
        /// <param name="path"></param>
        /// <param name="assembly"></param>
        /// <param name="classname"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        static public object LoadAssembly(string path, string assembly, string classname, string method, object[] parametors)
        {
            List<string> classList = new List<string>();

            //类型的完全限定名
            string fullName = assembly + "." + classname;

            //使用绝对路径读取程序集文件
            Assembly plugAssembly = Assembly.LoadFile(path + assembly);
            Type type = plugAssembly.GetType(fullName, true);
            MethodInfo methodinfo = type.GetMethod(method);

            object obj = plugAssembly.CreateInstance(fullName, true);
            object result = null;

            if (methodinfo != null)
                result = methodinfo.Invoke(methodinfo.IsStatic ? null : obj, parametors);

            return result;
        }
    }
}
