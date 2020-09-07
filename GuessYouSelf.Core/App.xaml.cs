using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace GuessYouSelf.Core
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var text = e.ExceptionObject.ToString();
                File.AppendAllText($"error[{DateTime.Now:yyyy-MM-ddThh-mm-ss}].log", text);
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }
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

        private void Application_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (e.Exception is WebException)
            {
                MessageBox.Show("Страница " + e.Uri.ToString() + " не может быть открыта.");
                e.Handled = true;
            }
        }
    }
}
