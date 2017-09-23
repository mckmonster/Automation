using System.Windows;

namespace GraphX_test
{
    public class Property : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Property));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(Property));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}