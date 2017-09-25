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
using Automation.Core;

namespace Automation.App
{
    /// <summary>
    /// Logique d'interaction pour ChooseJobType.xaml
    /// </summary>
    public partial class ChooseJobType : Window
    {
        public Job SelectedJob
        {
            get
            {
                var type = joblist.SelectedItem as Type;
                return JobFactory.CreateJob(type.FullName);
            }
        }

        public ChooseJobType()
        {
            InitializeComponent();

            joblist.ItemsSource = JobFactory.AvailableTypes();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
