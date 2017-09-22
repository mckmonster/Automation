﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphX_test
{
    public class GraphExecute
    {
        public static void Execute(MyGraph graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                foreach (var outEdge in graph.OutEdges(vertex))
                {
                    outEdge.Target.RegisterFinished(vertex);
                }                
            }

            foreach (var vertex in graph.Vertices)
            {
                if (graph.IsInEdgesEmpty(vertex))
                {
                    vertex.Launch();
                }
            }
        }
    }
}