using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphX_test
{
    public class GraphExecute
    {
        public static void Execute(MyGraph graph, bool _retry = false)
        {
            foreach (var vertex in graph.Vertices)
            {
                if (!_retry)
                {
                    vertex.State = JobState.NONE;
                }

                foreach (var outEdge in graph.OutEdges(vertex))
                {
                    outEdge.Target.RegisterFinished(vertex);
                }
                if (graph.IsInEdgesEmpty(vertex))
                {
                    vertex.Launch();
                }                
            }
        }
    }
}
