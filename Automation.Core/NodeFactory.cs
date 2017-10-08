using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public static class NodeFactory
    {
        public static List<Type> AvailableTypes { get; } = new List<Type>();

        static NodeFactory()
        {
            var folder = Path.Combine(Environment.CurrentDirectory, "plugins");
            var files = Directory.GetFiles(folder, "*.dll");
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract)
                    {
                        continue;
                    }

                    if (type.GetInterface("INode") != null)
                    {
                        AvailableTypes.Add(type);
                    }
                }
            }
        }

        public static INode CreateJob(string type)
        {
            var tp = GetType(type);
            return Activator.CreateInstance(tp) as INode;
        }

        public static Type GetType(string type)
        {
            return AvailableTypes.Find(_type => _type.FullName.Equals(type, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
