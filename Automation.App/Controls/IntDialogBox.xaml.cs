using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Automation.App.Controls
{
    /// <summary>
    /// Logique d'interaction pour IntDialogBox.xaml
    /// </summary>
    public partial class IntDialogBox : Window
    {
        private static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(int), typeof(IntDialogBox));
        public int SelectedValue
        {
            get
            {
                return (int)GetValue(SelectedValueProperty);
            }
            set
            {
                SetCurrentValue(SelectedValueProperty, value);
            }
        }

        public IntDialogBox()
        {
            InitializeComponent();
            SelectedValue = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
