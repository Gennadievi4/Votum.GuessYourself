using System.Threading;
using System.Windows;

namespace GuessYouSelf.Core
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (!InstanceCheck())
            {
                MessageBox.Show("Приложение уже запущено!!!",
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                Shutdown();
                return;
            }
        }

        private static Mutex InstanceCheckMutex;
        private static bool InstanceCheck()
        {
            bool isOnlyInstance;
            InstanceCheckMutex = new Mutex(true, @"Guess.Yourself", out isOnlyInstance);
            return isOnlyInstance;
        }
    }
}
