using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        static ManualResetEvent manualReset = new ManualResetEvent(false);
        static EventWaitHandle eventWait = manualReset;
        public static void FindExe()
        {
            var rep = Enumerable.Repeat(true, 10).Distinct();
            Enumerable.Range(20, 50).Select((item, index) => new { Item = item, Index = index }).ToList().ForEach(x => Console.WriteLine($"{x.Item} и {x.Index} {Task.CurrentId} Поток{Thread.CurrentThread.ManagedThreadId}"));
            eventWait.Set();
        }

        static public void SetStringToCenterConsole()
        {
            string[] fio = { "Фамилия", "Имя", "Отчество", "Город" };

            var paddingWidth = fio.Select(x => Console.WindowWidth / 2 - x.Length / 2).ToArray();
            var paddingHeight = Console.WindowHeight / 2 - fio.Length / 2;

            eventWait.WaitOne();

            for (int i = 0; i < fio.Length; i++)
            {
                Console.SetCursorPosition(paddingWidth[i], ++paddingHeight);
                Console.WriteLine(fio[i] + $" Поток №{Thread.CurrentThread.ManagedThreadId} Задача №{Task.CurrentId}");
            }
        }

        static public async Task SetStringToCenterConsoleAsync()
        {
            await Task.Run(SetStringToCenterConsole);
        }

        static void Main(string[] args)
        {
            SetStringToCenterConsoleAsync();
            FindExe();
            Console.ReadKey();
        }
    }
}
