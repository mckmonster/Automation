using Automation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automation.App
{
    public static class GraphDuplicateNode
    {
        public static void DuplicateNodes(this MyArea area, List<Job> nodes)
        {
            var graph = area.LogicCore.Graph;
            foreach (var node in nodes)
            {
                var newnodes = node.Duplicate(3);
                foreach (var newnode in newnodes)
                {
                    area.AddJob(newnode);

                    foreach (var inEdge in graph.InEdges(node))
                    {
                        if (!nodes.Contains(inEdge.Source))
                        {
                            area.AddLink(inEdge.Source, newnode);
                        }
                    }

                    foreach (var outEdge in graph.OutEdges(node))
                    {
                        if (!nodes.Contains(outEdge.Target))
                        {
                            area.AddLink(newnode, outEdge.Target);
                        }
                    }
                }
            }
            area.GenerateGraph();

            MessageBox.Show("Duplication Finished");
        }
    }
}
