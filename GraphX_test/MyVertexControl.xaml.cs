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
                var attr = prop.GetCustomAttributes(typeof(EditorAttribute), true);
                if (attr.Length > 0 )
                {
                    var property = new Property()
                    {
                        Name = prop.Name,
                        Value = prop.GetValue(job)
                    };

                    properties.Items.Add(property);
                }
            }
        }
    }
}
