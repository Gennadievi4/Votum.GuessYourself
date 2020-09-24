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
            var files = Directory.GetFiles(Environment.GetEnvironmentVariable("TEMP"));
            int i = 0;
            foreach (var item in files)
            {
                if(item.Contains("dd_vcredist_"))
                    Console.WriteLine($"{item} {i++}");
            }
            //foreach (var item in files)
            //{
            //    if (item.EndsWith(".exe.config") || item.EndsWith(".InstallState"))
            //        File.Delete(item);
            //}
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
            Console.ReadKey();
        }
    }
}
