using Automation.Core;
using Automation.Core.Attributes;
using System.Windows;

namespace Automation.App.Helper
{
    public static class GraphPropagate
    {
        private static int propagateInProgress;
        public static void PropagateNodeProperty(this MyGraph graph, Job node)
        {
            propagateInProgress++;
            foreach (var edge in graph.OutEdges(node))
            {
                var target = edge.Target;
                foreach (var property in node.GetType().GetProperties())
                {
                    var editable = (EditableAttribute[])property.GetCustomAttributes(typeof(EditableAttribute), true);
                    if (editable.Length == 0)
                    {
                        continue;
                    }
                    if (editable[0].ReadOnly)
                    {
                        continue;
                    }

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
            propagateInProgress--;
            if (propagateInProgress == 0)
            {
                MessageBox.Show("Propagate Finished");
            }
        }
    }
}
