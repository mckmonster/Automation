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
        private static List<Assembly> m_assembly = new List<Assembly>();

        static NodeFactory()
        {
            var folder = Path.Combine(Environment.CurrentDirectory, "plugins");
            var files = Directory.GetFiles(folder, "*.dll");
            foreach (var file in files)
            {
                 m_assembly.Add(Assembly.LoadFile(file));
            }
        }

        public static INode CreateJob(string type)
        {
            foreach (var assembly in m_assembly)
            {
                if (assembly.GetType(type) != null)
                {
                    var node = assembly.CreateInstance(type);
                    return node as INode;
                }
            }
            return null;
        }

        public static Type GetType(string type)
        {
            var assembly = m_assembly.Where(_assembly => _assembly.GetType(type) != null);
            return assembly.First().GetType(type);
        }

        public static List<Type> AvailableTypes()
        {
            var list = new List<Type>();
            foreach (var assembly in m_assembly)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract)
                    {
                        continue;
                    }

                    if (type.GetInterface("INode") != null)
                    {
                        list.Add(type);
                    }
                }
            }
            return list;
        }
    }
}
