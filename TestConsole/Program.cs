using HidApiAdapter;
using RLib;
using RLib.Infrastructure;
using RLib.Remotes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestConsole.BaseVotumDevices;

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

        static int receiver = 8681;

        static int remote = 3;

        static DeviceManager deviceManager = new DeviceManager();

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
            eventWait.Set();
        }

        static public async void SetStringToCenterConsoleAsync()
        {
            await Task.Run(() =>
            {
                string[] fio = { "Фамилия", "Имя", "Отчество", "Город" };

                var paddingWidth = fio.Select(x => Console.WindowWidth / 2 - x.Length / 2).ToArray();
                var paddingHeight = Console.WindowHeight / 2 - fio.Length / 2;

                eventWait.WaitOne();

                for (int i = 0; i < fio.Length; i++)
                {
                    Console.SetCursorPosition(paddingWidth[i], ++paddingHeight);
                    Console.WriteLine(fio[i]);
                }
            });
        }

        static async Task Main(string[] args)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var path2 = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            //var path3 = Assembly.GetExecutingAssembly().GetName().CodeBase;
            //var path6 = Environment.GetEnvironmentVariable("TEMP");
            //var path4 = Process.GetCurrentProcess().MainModule.FileName;
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "Животные1.txt");
            //var path7 = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Животные1.txt");


            //SetStringToCenterConsoleAsync();
            //FindExe();


            //Source s = new Source();
            //Observer1 o1 = new Observer1();
            //Observer2 o2 = new Observer2();
            //s.Run += o1.Do;
            //s.Run += o2.Do;
            //s.Start();
            //s.Run -= o1.Do;
            //s.Start();

            try
            {
                #region reflecsion
                //deviceManager = new DeviceManager();
                //deviceManager.VotumDevicesManager.ButtonClicked += VotumDevicesManager_ButtonClicked;

                //DeviceRepresentation votumDevice = deviceManager.VotumDevicesManager.Devices.First();

                //IDevice votumHidDevice = votumDevice.Device;

                //var strategy = votumDevice
                //    .GetType()
                //    .GetField("m_Strategy", BindingFlags.NonPublic | BindingFlags.Instance)
                //    .GetValue(votumDevice);

                //MethodInfo sendbackCommandMethod = strategy
                //    .GetType()
                //    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                //    .Single(x => x.Name == "SendCommandToRemote" && x.GetParameters().Length == 2);

                //var IsRunning = strategy
                //    .GetType()
                //    .GetProperty("IsRunning", BindingFlags.Public | BindingFlags.Instance)
                //    .GetValue(strategy);

                //var connectedVotumDevice = votumHidDevice
                //    .GetType()
                //    .GetField("m_Device", BindingFlags.NonPublic | BindingFlags.Instance)
                //    .GetValue(votumHidDevice);

                //var IsConnectedVotumDevice = connectedVotumDevice
                //    .GetType()
                //    .GetProperty("IsConnected", BindingFlags.Public | BindingFlags.Instance)
                //    .GetValue(connectedVotumDevice);

                //if ((bool)IsRunning == true)
                //    sendbackCommandMethod.Invoke(strategy, new object[] { votumHidDevice, remotePacket });

                //if ((bool)IsConnectedVotumDevice == true)
                //{
                //    Thread.Sleep(100);
                //    sendbackCommandMethod.Invoke(strategy, new object[] { votumHidDevice, remotePacket });
                //}

                //sendbackCommandMethod.Invoke(strategy, new object[] { votumHidDevice, receiver, remote, RemoteCommand.CMD_DISPLAY_LOGO });
                #endregion

                SendbackCommand sendbackCommand = new SendbackCommand(receiver, remote, RemoteCommand.CMD_DISPLAY_LOGO);

                deviceManager.VotumDevicesManager.SendCommandToRemote(sendbackCommand);

                ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
                using (CancellationTokenSource cts = new CancellationTokenSource())
                {
                    Task task = LoopAsync(cts.Token, queue);
                    string text;
                    while ((text = Console.ReadLine()).Length > 0)
                    {
                        queue.Enqueue(text + Thread.CurrentThread.ManagedThreadId);
                    }
                    cts.Cancel();
                    await task;
                    Console.WriteLine("Done.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        private static void VotumDevicesManager_ButtonClicked(object sender, ButtonClickEventArgs e)
        {
            Console.WriteLine(e.Battary);

            receiver = e.ReceiverId;

            remote = e.RemoteId;
        }

        private static async Task RemoteWait()
        {

        }

        private static async Task LoopAsync(CancellationToken token, ConcurrentQueue<string> queue)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Начало работы метода с циклом в потоке №{Thread.CurrentThread.ManagedThreadId} и задаче №{Task.CurrentId}");
                while (!token.IsCancellationRequested)
                {
                    if (queue.TryDequeue(out string text))
                        Console.WriteLine("!!!!!=" + text);
                    else
                        Console.WriteLine("ping");
                    Thread.Sleep(1000);
                }
            });
            Console.WriteLine($"Конец работы метода с циклом в потоке №{Thread.CurrentThread.ManagedThreadId} и задаче №{(Task.CurrentId == null ? 0 : Task.CurrentId)}");
        }
    }
}
