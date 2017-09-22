using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraphX_test
{
    /// <summary>
    /// Logique d'interaction pour ChooseJobType.xaml
    /// </summary>
    public partial class ChooseJobType : Window
    {
        private Assembly assembly = Assembly.GetAssembly(typeof(Job));
        public Job SelectedJob
        {
            get
            {
                var type = joblist.SelectedItem as Type;
                return assembly.CreateInstance(type.FullName) as Job;
            }
        }

        public ChooseJobType()
        {
            InitializeComponent();

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (IsJobType(type))
                {
                    joblist.Items.Add(type);
                }
            }
        }

        private bool IsJobType(Type type)
        {
            if (type == null)
            {
                return false;
            }
            if (type == typeof(Job))
            {
                return true;
            }
            return IsJobType(type.BaseType);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
