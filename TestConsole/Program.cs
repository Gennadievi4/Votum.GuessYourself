using RLib;
using RLib.Remotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TestConsole.BaseVotum;

namespace TestConsole
{
    public class Program
    {
        private static bool SyncFlag = true;

        private static VotumDevicesManager _VotumDevicesManager = new VotumDevicesManager();

        private static Thread thread = new Thread(LongTimeRemote) { IsBackground = true };

        private static void Main(string[] args)
        {
            Init();

            _VotumDevicesManager.ButtonClicked += BaseVotumDevice_ClickButtonsRemotes;

            RunWork();
        }

        private static void Init()
        {
            _VotumDevicesManager.Settings.InteractiveRemotesSettings.InteractiveMode = true;
            _VotumDevicesManager.Settings.InteractiveRemotesSettings.AutoT2 = true;
            _VotumDevicesManager.Start();
        }

        private static void LongTimeRemote()
        {
            SendbackCommand wait = new SendbackCommand(8681, 1, RemoteCommand.CMD_WAIT);

            while (SyncFlag)
            {
                _VotumDevicesManager.SendCommandToRemote(wait);
                Thread.Sleep(16);
            }
        }

        private static void BaseVotumDevice_ClickButtonsRemotes(object sender, ButtonClickEventArgs e)
        {
            Console.WriteLine(e.Battary);

            e.SendbackCommand = new SendbackCommand(e.ReceiverId, e.RemoteId, RemoteCommand.CMD_WAIT);

            thread.Start();
        }

        private static void RunWork()
        {
            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    SendbackCommand logo = new SendbackCommand(8681, 1, RemoteCommand.CMD_DISPLAY_LOGO);
                    _VotumDevicesManager.SendCommandToRemote(logo);
                    SyncFlag = false;
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
