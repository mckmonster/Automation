using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraphX_test
{
    public class PropertyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate BoolDataTemplate { get; set; }
        public DataTemplate ReadOnlyTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PropertyInfo propInfo)
            {
                if (propInfo.ReadOnly)
                {
                    return ReadOnlyTemplate;
                }
                if (propInfo.Value is bool)
                {
                    return BoolDataTemplate;
                }

                return DefaultDataTemplate;
            }
            
            return base.SelectTemplate(item, container);
        }
    }
}
