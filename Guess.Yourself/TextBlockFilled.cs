using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class TextBlockFilled : Control
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text),
    typeof(string),
    typeof(TextBlockFilled));

    public string Text
    {
        get { return GetValue(TextProperty) as string; }
        set { SetValue(TextProperty, value); }
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        FormattedText formattedText = formattedText = new FormattedText(Text, 
            CultureInfo.GetCultureInfo("en-us"), 
            FlowDirection.LeftToRight, 
            new Typeface("Verdana"),
            ActualHeight,
            Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);

        formattedText.TextAlignment = TextAlignment.Center;
        formattedText.MaxTextWidth = ActualWidth - Padding.Left - Padding.Right;
        formattedText.Trimming = TextTrimming.None;
        double step = ActualHeight / 20;

        for (double i = ActualHeight - step; i >= step; i -= step)
        {
            if (formattedText.Height <= ActualHeight - Padding.Top - Padding.Bottom) break;
            formattedText.SetFontSize(i);
        }
        
        formattedText.MaxTextHeight = ActualHeight;
        drawingContext.DrawText(formattedText, new Point(Padding.Left, Padding.Top));
    }
}