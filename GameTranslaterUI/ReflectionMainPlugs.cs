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
        static public bool LoadAssembly(string path)
        {
            Assembly ass;
            Type type;
            Object obj;

            string[] file = Directory.GetFiles(path);

            Object any = new Object();
            ass = Assembly.LoadFile(file[0]);
            type = ass.GetType("ReflectionTest.WriteTest");
            return true;
        }
    }
}
