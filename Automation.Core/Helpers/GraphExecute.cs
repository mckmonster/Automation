using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Helpers
{
    public static class GraphExecute
    {
        private static bool m_launched = false;

        public static event Action OnFinished;

        public static void Execute(this MyGraph graph, bool _retry = false)
        {
            _nbVertices = graph.Vertices.Count();
            _nbVerticesStopped = 0;
            m_launched = true;
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

                vertex.OnFinished += Vertex_OnFinished;
            }

            foreach (var vertex in graph.Vertices)
            {
                if (graph.IsInEdgesEmpty(vertex))
                {
                    vertex.Launch();
                }
            }
        }

        private static int _nbVerticesStopped = 0;
        private static int _nbVertices = 0;
        private static void Vertex_OnFinished(MyVertex obj)
        {
            _nbVerticesStopped++;
            obj.OnFinished -= Vertex_OnFinished;
            m_launched = _nbVerticesStopped != _nbVertices;
            if (!m_launched)
            {
                log4net.LogManager.GetLogger("Automation.Core").Info("Graph execution finished");
                OnFinished?.Invoke();
            }
        }

        public static void Cancel(this MyGraph graph)
        {
            if (m_launched)
            {
                foreach (var vertex in graph.Vertices)
                {
                    vertex.Cancel();
                }
            }
        }
    }
}
