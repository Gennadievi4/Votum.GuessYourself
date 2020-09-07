using System.Diagnostics;
using System.Windows;

namespace GuessYouSelf.Core
{
    public partial class AboutGuessYourself
    {
        public AboutGuessYourself() => InitializeComponent();
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
