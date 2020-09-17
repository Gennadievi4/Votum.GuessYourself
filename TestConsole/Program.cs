using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TestConsole
{
    class Program
    {
        public static void FindExe()
        {
            var files = Directory.GetFiles(Path.GetFullPath(@"C:\Users\gonzy\source\repos\Votum.Interactive.Games\Guess.Yourself\bin\Debug"));
            foreach (var item in files)
            {
                if (item.EndsWith(".exe.config") || item.EndsWith(".InstallState"))
                    File.Delete(item);
            }
        }

        static void Main(string[] args)
        {
            var path2 = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var path3 = Assembly.GetExecutingAssembly().GetName().CodeBase;
            var path6 = Environment.GetEnvironmentVariable("TEMP");
            var path4 = Process.GetCurrentProcess().MainModule.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Животные1.txt");
            var path7 = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Животные1.txt");
            FindExe();
            Console.WriteLine(path7);
            Console.ReadKey();
        }
    }
}
