using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Automation.Core;

namespace GraphX_test
{
    [ValueConversion(typeof(JobState), typeof(Brush))]
    public class JobStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is JobState state)
            {
                var color = Colors.Black;
                switch (state)
                {
                    case JobState.NONE:
                        color = Colors.Black;
                        break;
                    case JobState.INPROGRESS:
                        color = Colors.Orange;
                        break;
                    case JobState.SUCCEED:
                        color = Colors.Green;
                        break;
                    case JobState.FAILED:
                        color = Colors.Red;
                        break;
                    default:
                        throw new ArgumentException($"{state} is not define");
                }

                return new SolidColorBrush(color);
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
