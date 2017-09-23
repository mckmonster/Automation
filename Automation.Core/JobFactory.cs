using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public class JobFactory
    {
        private Assembly assembly = Assembly.LoadFile(System.IO.Path.Combine(Environment.CurrentDirectory, "Automation.Test.Plugin.dll"));

        public Job CreateJob(string type)
        {
            var job = assembly.CreateInstance(type);
            return job as Job;
        }
    }
}
