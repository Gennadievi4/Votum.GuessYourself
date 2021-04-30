using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestWPF.Core
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestLbl.Content = "Start";
            DelayAsync().Wait();
            TestLbl.Content = "Stop";
        }

        private async Task DelayAsync() => await new Task(() => Thread.Sleep(2000)).ConfigureAwait(true);
    }
}
