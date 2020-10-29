using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestConsole
{
    class Program
    {
        #region events
        //class MyEventArgs : EventArgs
        //{
        //    public string StartSource { get; }
        //    public MyEventArgs(string s)
        //    {
        //        StartSource = s;
        //    }
        //}
        //class Source
        //{
        //    public event EventHandler<MyEventArgs> Run;
        //    public void Start()
        //    {
        //        //Console.WriteLine("RUN");
        //        Run?.Invoke(this, new MyEventArgs($"Я {this.GetType().Name} уже бегу!"));
        //    }
        //}
        //class Observer1
        //{
        //    public void Do(object sender, MyEventArgs e) => Console.WriteLine($"Первый \"Наблюдатель\" - {this.GetType().Name} видит, что другой объект сообщил: {e.StartSource}");

        //}
        //class Observer2
        //{
        //    public void Do(object sender, MyEventArgs e) => Console.WriteLine($"Второй \"Наблюдатель\" - {this.GetType().Name} видит, что другой объект сообщил: {e.StartSource}");

        //}
        #endregion 
        public static void FindExe()
        {
            //var files = Directory.GetFiles(Environment.GetEnvironmentVariable("TEMP"));
            //int i = 0;
            //foreach (var item in files)
            //{
            //    if(item.Contains("dd_vcredist_"))
            //        Console.WriteLine($"{item} {i++}");
            //}
            //foreach (var item in files)
            //{
            //    if (item.EndsWith(".exe.config") || item.EndsWith(".InstallState"))
            //        File.Delete(item);
            //}
            var rep = Enumerable.Repeat(true, 10).Distinct();
            Enumerable.Range(20, 50).Select((item, index) => new { Item = item, Index = index }).ToList().ForEach(x => Console.WriteLine($"{x.Item} и {x.Index}"));
        }

        static public void SetStringToCenterConsole()
        {
            string[] fio = { "Фамилия", "Имя", "Отчество", "Город" };

            var paddingWidth = fio.Select(x => Console.WindowWidth / 2 - x.Length / 2).ToArray();
            var paddingHeight = Console.WindowHeight / 2 - fio.Length / 2;

            for (int i = 0; i < fio.Length; i++)
            {
                Console.SetCursorPosition(paddingWidth[i], ++paddingHeight);
                Console.WriteLine(fio[i]);
            }
        }

        static void Main(string[] args)
        {
            //var path2 = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            //var path3 = Assembly.GetExecutingAssembly().GetName().CodeBase;
            //var path6 = Environment.GetEnvironmentVariable("TEMP");
            //var path4 = Process.GetCurrentProcess().MainModule.FileName;
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "Животные1.txt");
            //var path7 = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Животные1.txt");
            //SetStringToCenterConsole();
            FindExe();

            //Source s = new Source();
            //Observer1 o1 = new Observer1();
            //Observer2 o2 = new Observer2();
            //s.Run += o1.Do;
            //s.Run += o2.Do;
            //s.Start();
            //s.Run -= o1.Do;
            //s.Start();

            Console.ReadKey();
        }
    }
}
