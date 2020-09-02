using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Guess.Yourself.Converters
{
    class ActualHeightTriggerToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double size && size <= 1000 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
