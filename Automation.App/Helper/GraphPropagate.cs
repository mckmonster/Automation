using Automation.Core;
using System;
using System.ComponentModel;
using System.Windows;

namespace Automation.App.Helper
{
    public static class GraphPropagate
    {
        private static int propagateInProgress;
        public static void PropagateNodeProperty(this MyGraph graph, MyVertex node)
        {
            propagateInProgress++;
            foreach (var edge in graph.OutEdges(node))
            {
                var source = node.Job;
                var target = edge.Target.Job;
                foreach (var property in source.GetType().GetProperties())
                {
                    var readOnlyAttr = (ReadOnlyAttribute)Attribute.GetCustomAttribute(property, typeof(ReadOnlyAttribute));
                    if (readOnlyAttr != null && readOnlyAttr.IsReadOnly)
                    {
                        continue;
                    }

                    if (property.GetValue(source) == null)
                    {
                        continue;
                    }
                    try
                    {
                        var targetproperty = target.GetType().GetProperty(property.Name);
                        //if (targetproperty.GetValue(target) == null)
                        {
                            targetproperty.SetValue(target, property.GetValue(source));
                            target.RaisePropertyChanged(targetproperty.Name);
                        }
                    }
                    catch
                    {

                    }
                }

                graph.PropagateNodeProperty(edge.Target);
            }
            propagateInProgress--;
            if (propagateInProgress == 0)
            {
                MessageBox.Show("Propagate Finished");
            }
        }
    }
}
