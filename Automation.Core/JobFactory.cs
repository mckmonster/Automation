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

        public static List<Type> AvailableTypes()
        {
            var list = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                list.Add(type);
            }
            return list;
        }
    }
}
