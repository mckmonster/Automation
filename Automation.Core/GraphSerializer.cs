using QuickGraph.Algorithms;
using QuickGraph.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Automation.Core
{
    public static class GraphSerializer
    {
        public static void Serialize(Stream stream, MyGraph graph)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true
            };
            using (var writer = XmlWriter.Create(stream, settings))
            {
                graph.SerializeToXml(writer, AlgorithmExtensions.GetVertexIdentity(graph), AlgorithmExtensions.GetEdgeIdentity(graph), "MyGraph", "Job", "MyEdge", "",
                    (wr, gr) => { },
                    SerializeNode,
                    (wr, ed) => { }
                );
            }
            stream.Close();
        }

        private static List<Job> jobs = new List<Job>();
        public static MyGraph DeSerialize(Stream stream)
        {
            jobs.Clear();
            using (var reader = XmlReader.Create(stream))
            {
                return reader.DeserializeFromXml<Job, MyEdge, MyGraph>("MyGraph", "Job", "MyEdge", "",
                    rd => { return new MyGraph(); },
                    DeserializeNode,
                    rd =>
                    {
                        var source = jobs.Find(job => job.ID.Equals(long.Parse(rd.GetAttribute("source"))));
                        var target = jobs.Find(job => job.ID.Equals(long.Parse(rd.GetAttribute("target"))));
                        return new MyEdge(source, target);
                    });
            }
        }

        private static void SerializeNode(XmlWriter wr, Job vr)
        {
            wr.WriteAttributeString("type", vr.GetType().FullName);

            var serializer = new YAXLib.YAXSerializer(vr.GetType());
            var result = serializer.Serialize(vr);
            wr.WriteRaw(result);
        }

        private static Job DeserializeNode(XmlReader rd)
        {
            var typestring = rd.GetAttribute("type");
            var type = JobFactory.GetType(typestring);
            var serializer = new YAXLib.YAXSerializer(type);
            var id = long.Parse(rd.GetAttribute("id"));
            var job = serializer.Deserialize(rd.ReadInnerXml()) as Job;
            job.ID = id;
            jobs.Add(job);
            
            return job;
        }
    }
}
