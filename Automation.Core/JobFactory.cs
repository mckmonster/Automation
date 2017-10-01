using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public static class JobFactory
    {
        private static Assembly assembly = Assembly.LoadFile(System.IO.Path.Combine(Environment.CurrentDirectory, "Automation.Test.Plugin.dll"));

        public static IJob CreateJob(string type)
        {
            var job = assembly.CreateInstance(type);
            return job as IJob;
        }
        public static Type GetType(string type)
        {
            return assembly.GetType(type);
        }

        public static List<Type> AvailableTypes()
        {
            var list = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.GetInterface("IJob") != null)
                {
                    list.Add(type);
                }
            }
            return list;
        }
    }
}
