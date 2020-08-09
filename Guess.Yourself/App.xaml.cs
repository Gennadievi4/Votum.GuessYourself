using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace Guess.Yourself
{
    public partial class App : Application
    {
        public App()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var text = e.ExceptionObject.ToString();
                File.AppendAllText($"error[{DateTime.Now:yyyy-MM-ddThh-mm-ss}].log", text);
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }

        //private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    var current_Assembly = Assembly.GetExecutingAssembly();
        //    var requiredDllName = $"{(new AssemblyName(args.Name).Name)}.dll";
        //    var resources = current_Assembly.GetManifestResourceNames().Where(s => s.EndsWith(requiredDllName)).FirstOrDefault();

        //    if (resources != null)
        //    {
        //        using (var stream = current_Assembly.GetManifestResourceStream(resources))
        //        {
        //            if (stream != null)
        //                return null;

        //            var block = new byte[stream.Length];
        //            stream.Read(block, 0, block.Length);
        //            return Assembly.Load(block);
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
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
            if(e.Exception is WebException)
            {
                MessageBox.Show("Страница " + e.Uri.ToString() + " не может быть открыта.");
                e.Handled = true;
            }
        }
    }
}
