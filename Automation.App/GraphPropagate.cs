using Automation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.App
{
    public static class GraphPropagate
    {
        public static void PropagateNodeProperty(this MyGraph graph, Job node)
        {
            foreach (var edge in graph.OutEdges(node))
            {
                var target = edge.Target;
                foreach (var property in node.GetType().GetProperties())
                {
                    if (property.GetValue(node) == null)
                    {
                        continue;
                    }
                    try
                    {
                        var targetproperty = target.GetType().GetProperty(property.Name);
                        //if (targetproperty.GetValue(target) == null)
                        {
                            targetproperty.SetValue(target, property.GetValue(node));
                            target.RaisePropertyChanged(targetproperty.Name);
                        }
                    }
                    catch
                    {

                    }
                }

                graph.PropagateNodeProperty(target);
            }
        }
    }
}
