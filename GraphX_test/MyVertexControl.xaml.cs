using Automation.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphX_test
{
    /// <summary>
    /// Logique d'interaction pour MyVertexControl.xaml
    /// </summary>
    public partial class MyVertexControl : UserControl
    {
        public MyVertexControl()
        {
            InitializeComponent();

            DataContextChanged += MyVertexControl_DataContextChanged;
        }

        private void MyVertexControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var job = e.NewValue;
            var type = job.GetType();

            foreach (var prop in type.GetProperties())
            {
                var attr = (EditableAttribute[])prop.GetCustomAttributes(typeof(EditableAttribute), true);

                if (attr.Length > 0)
                {
                    Binding bind = new Binding()
                    {
                        Path = new PropertyPath(prop.Name),
                        Source = job,
                        Mode = BindingMode.TwoWay
                    };

                    var property = new PropertyInfo()
                    {
                        Name = prop.Name,
                        ReadOnly = attr[0].ReadOnly
                    };
                    BindingOperations.SetBinding(property, PropertyInfo.ValueProperty, bind);

                    properties.Items.Add(property);
                }
            }
        }
    }
}
