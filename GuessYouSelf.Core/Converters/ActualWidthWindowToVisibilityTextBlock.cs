﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GuessYourself.Core.Converters
{
    public class ActualWidthWindowToVisibilityTextBlock : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double size && size <= 1150 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}