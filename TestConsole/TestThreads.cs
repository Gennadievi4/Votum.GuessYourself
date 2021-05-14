using System;
using System.Threading;

namespace TestConsole
{
    public static class TestThreads
    {
        private static ManualResetEvent manual_event = new ManualResetEvent(true);

        private static Thread thread = thread = new Thread(
                (x) =>
                {
                    ManualResetEvent mre = (ManualResetEvent)x;

                    while (true)
                    {
                        mre.WaitOne();
                        Thread.Sleep(300);
                        Console.WriteLine("Запущен тест!");
                    }
                });


        public static void Start()
        {
            if (thread.IsAlive)
                return;

            thread.Start(manual_event);
        }

        public static void Stop()
        {
            manual_event.Reset();
        }

        public static void Run()
        {
            if (thread.ThreadState == ThreadState.Stopped && thread.ThreadState != ThreadState.Running || thread.ThreadState == ThreadState.WaitSleepJoin)
                manual_event.Set();
        }
    }
}
