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

        public static Job CreateJob(string type)
        {
            var job = assembly.CreateInstance(type);
            return job as Job;
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

                if (IsJobType(type))
                {
                    list.Add(type);
                }
            }
            return list;
        }

        private static bool IsJobType(Type type)
        {
            if (type == null)
            {
                return false;
            }
            if (type == typeof(Job))
            {
                return true;
            }
            return IsJobType(type.BaseType);
        }
    }
}
