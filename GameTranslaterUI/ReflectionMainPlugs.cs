﻿using System;
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
        static Assembly m_plugAssembly = null;

        static public bool LoadAssembly(string path,string name)
        {
           
            Type type;
            Object obj;

 

            Object any = new Object();
            m_plugAssembly = Assembly.LoadFile(path+name);
            //type = m_plugAssembly.GetType();

            Type[] ts = m_plugAssembly.GetTypes();
            return true;
        }
    }
}
