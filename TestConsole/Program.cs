using RLib;
using RLib.Remotes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestConsole.BaseVotum;

namespace TestConsole
{
    public class Program
    {
        private static bool SyncFlag = true;

        private static VotumDevicesManager _VotumDevicesManager = new VotumDevicesManager();

        private static Task t = new Task(async () => await LoopSendBackCommands2(src.Token).ConfigureAwait(true));

        private static Stopwatch stopwatch = new Stopwatch();

        private static CancellationTokenSource src = new CancellationTokenSource();

        private static ConcurrentQueue<SendbackCommand> sendbackCommands = new ConcurrentQueue<SendbackCommand>();

        //private static QueueHandler<SendbackCommand> QueueHandler = new QueueHandler<SendbackCommand>((number, hand) =>
        //{
        //    while (!hand.IsCancellationRequested)
        //    {
        //        _VotumDevicesManager.SendCommandToRemote(number);
        //        Thread.Sleep(10);
        //    }
        //});
        private static Thread thread = new Thread(LongTimeRemote) { IsBackground = true, };

        private static SendbackCommand wait = new SendbackCommand(8681, 1, RemoteCommand.CMD_WAIT);

        private static SendbackCommand logo = new SendbackCommand(8681, 1, RemoteCommand.CMD_DISPLAY_LOGO);

        private static void Main(string[] args)
        {
            Init();

            _VotumDevicesManager.ButtonClicked += BaseVotumDevice_ClickButtonsRemotes;

            RunWork();

            Console.ReadKey();
        }

        private static void Init()
        {
            _VotumDevicesManager.Settings.InteractiveRemotesSettings.InteractiveMode = true;
            _VotumDevicesManager.Settings.InteractiveRemotesSettings.AutoT2 = true;
            _VotumDevicesManager.Start();
        }

        private static void LongTimeRemote()
        {
            stopwatch.Start();

            Console.WriteLine($"Запущен поток {Thread.CurrentThread.ManagedThreadId} с отправкой команды \"Ждать\"");

            while (SyncFlag)
            {
                _VotumDevicesManager.SendCommandToRemote(wait);
                Thread.Sleep(20);
                Console.Title = string.Format("Вторичный поток работает {0:D}", (int)stopwatch.Elapsed.TotalSeconds);
            }

            Console.WriteLine($"Закончен поток {Thread.CurrentThread.ManagedThreadId} с отправкой команды \"Ждать\"");

            stopwatch.Stop();
        }

        private static void BaseVotumDevice_ClickButtonsRemotes(object sender, ButtonClickEventArgs e)
        {
            Console.WriteLine($"Нажата кнопка {e.Button} на пульте {e.RemoteId} в потоке {Thread.CurrentThread.ManagedThreadId}");

            e.SendbackCommand = new SendbackCommand(e.ReceiverId, e.RemoteId, RemoteCommand.CMD_WAIT);

            t.Start();

            //QueueHandler.AddItem(e.SendbackCommand);

            //thread.Start();
        }

        private async static void RunWork()
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.WriteLine($"Метод RunWork запущен в потоке {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Состояние задачи {(t.IsCompleted == true ? "завершено" : "не завершено")}");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine($"Метод RunWork блок Enter запущен в потоке {Thread.CurrentThread.ManagedThreadId}");

                    _VotumDevicesManager.SendCommandToRemote(logo);


                    src.Cancel();

                    await t;

                    Console.WriteLine($"Состояние задачи {(t.IsCompleted == true ? "завершено" : "не завершено")}");

                    SyncFlag = false;

                    _VotumDevicesManager.Stop();

                    //src.Cancel();
                    //QueueHandler.Dispose();
                }
            }
        }

        private static void LoopSendBackCommands(CancellationToken token)
        {
            Task.Run(() =>
            {
                Console.WriteLine($"Запущена работа асинхронной отправки команды \"ждать\" в потоке {Thread.CurrentThread.ManagedThreadId} задача: {Task.CurrentId}");

                while (!token.IsCancellationRequested)
                {
                    _VotumDevicesManager.SendCommandToRemote(wait);
                    Thread.Sleep(30);
                }

                Console.WriteLine($"Конец асинхронной операции отправки команды \"ждать\" в потоке {Thread.CurrentThread.ManagedThreadId} задача: {Task.CurrentId}");
            });
        }

        private async static Task LoopSendBackCommands2(CancellationToken token)
        {
            Console.WriteLine($"Запущена работа асинхронной отправки команды \"ждать\" в потоке {Thread.CurrentThread.ManagedThreadId} задача: {t.Id}");

            while (!token.IsCancellationRequested)
            {
                _VotumDevicesManager.SendCommandToRemote(wait);
                await Task.Delay(20);
            }

            Console.WriteLine($"Конец асинхронной операции отправки команды \"ждать\" в потоке {Thread.CurrentThread.ManagedThreadId} задача: {t.Id}");
        }
    }
}
