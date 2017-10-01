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
using YAXLib;

namespace Automation.Core.Helpers
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

        private static List<MyVertex> vertices = new List<MyVertex>();
        public static MyGraph DeSerialize(Stream stream)
        {
            MyGraph result;
            vertices.Clear();
            using (var reader = XmlReader.Create(stream))
            {
                result = reader.DeserializeFromXml<MyVertex, MyEdge, MyGraph>("MyGraph", "Job", "MyEdge", "",
                    rd => { return new MyGraph(); },
                    DeserializeNode,
                    rd =>
                    {
                        var source = vertices.Find(job => job.ID.Equals(long.Parse(rd.GetAttribute("source"))));
                        var target = vertices.Find(job => job.ID.Equals(long.Parse(rd.GetAttribute("target"))));
                        return new MyEdge(source, target);
                    });
            }
            stream.Close();
            return result;
        }

        private static void SerializeNode(XmlWriter wr, MyVertex vr)
        {
            wr.WriteAttributeString("type", vr.Job.GetType().FullName);

            var serializer = new YAXLib.YAXSerializer(vr.Job.GetType(), YAXSerializationOptions.DontSerializeNullObjects);
            var result = serializer.Serialize(vr);
            wr.WriteRaw(result);
        }

        private static MyVertex DeserializeNode(XmlReader rd)
        {
            var typestring = rd.GetAttribute("type");
            var type = JobFactory.GetType(typestring);
            var serializer = new YAXLib.YAXSerializer(type, YAXSerializationOptions.DontSerializeNullObjects);
            var id = long.Parse(rd.GetAttribute("id"));
            var job = serializer.Deserialize(rd.ReadInnerXml()) as IJob;
            var vertex = new MyVertex(job)
            {
                ID = id
            };
            vertices.Add(vertex);
            
            return vertex;
        }
    }
}
