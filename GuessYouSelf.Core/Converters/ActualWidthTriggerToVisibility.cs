using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GuessYourself.Core.Converters
{
    class ActualWidthTriggerToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double size && size <= 1150 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
