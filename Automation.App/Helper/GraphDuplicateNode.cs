using Automation.App.Gx;
using Automation.Core;
using System.Collections.Generic;
using System.Windows;

namespace Automation.App.Helper
{
    public static class GraphDuplicateNode
    { 
        public static void DuplicateNodes(this MyArea area, List<Job> nodes, int nbDuplicate)
        {
            Dictionary<Job, List<Job>> jobs = new Dictionary<Job, List<Job>>();

            var graph = area.LogicCore.Graph;
            foreach (var node in nodes)
            {
                jobs.Add(node, new List<Job>());

                var newnodes = node.Duplicate(nbDuplicate);
                foreach (var newnode in newnodes)
                {
                    area.AddJob(newnode);
                    jobs[node].Add(newnode);
                }
            }

            
            foreach (var node in nodes)
            {
                int i = 0;
                foreach (var newnode in jobs[node])
                {
                    foreach (var inedge in graph.InEdges(node))
                    {
                        if (!nodes.Contains(inedge.Source))
                        {
                            area.AddLink(inedge.Source, newnode);
                        }
                        else
                        {
                            var source = jobs[inedge.Source][i];
                            area.AddLink(source, newnode);
                        }
                    }

                    foreach (var outedge in graph.OutEdges(node))
                    {
                        if (!nodes.Contains(outedge.Target))
                        {
                            area.AddLink(newnode, outedge.Target);
                        }
                        else
                        {
                            var target = jobs[outedge.Target][i];
                            area.AddLink(newnode, target);
                        }
                    }

                    i++;
                }
            }

            area.GenerateGraph();

            MessageBox.Show("Duplication Finished");
        }
    }
}
