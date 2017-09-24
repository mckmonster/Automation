using QuickGraph.Algorithms;
using QuickGraph.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    (wr, vr) => {
                        wr.WriteAttributeString("type", vr.GetType().FullName);
                        //vr.Save(wr);
                    },
                    (wr, ed) => { }
                );
            }
        }

        public static MyGraph DeSerialize(Stream stream)
        {
            List<Job> jobs = new List<Job>();
            using (var reader = XmlReader.Create(stream))
            {
                return reader.DeserializeFromXml<Job, MyEdge, MyGraph>("MyGraph", "Job", "MyEdge", "",
                    rd => { return new MyGraph(); },
                    rd =>
                    {
                        var job = JobFactory.CreateJob(rd.GetAttribute("type"));
                        job.ID = long.Parse(rd.GetAttribute("id"));
                        jobs.Add(job);
                        return job;
                    },
                    rd =>
                    {
                        var source = jobs.Find(job => job.ID.Equals(long.Parse(rd.GetAttribute("source"))));
                        var target = jobs.Find(job => job.ID.Equals(long.Parse(rd.GetAttribute("target"))));
                        return new MyEdge(source, target);
                    });
            }
        }
    }
}
