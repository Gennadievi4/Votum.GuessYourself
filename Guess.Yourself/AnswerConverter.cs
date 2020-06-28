using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Guess.Yourself
{
    public class AnswerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ColorAnswer answer)
            {
                switch (answer)
                {
                    case ColorAnswer.Yes:
                        return new SolidColorBrush(Colors.Green);
                    case ColorAnswer.No:
                        return new SolidColorBrush(Colors.Red);
                    case ColorAnswer.DontKnow:
                        return new SolidColorBrush(Colors.Yellow);
                    default: throw new Exception("Не выбран цвет!");
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
