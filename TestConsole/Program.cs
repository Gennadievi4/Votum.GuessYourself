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

        private static readonly BaseVotumDevice baseVotumDevice = new BaseVotumDevice();

        private static Thread thread = new Thread(LongTimeRemote) { IsBackground = true };

        private static Remote remoteInRun;

        private static List<Remote> RemotesList = new List<Remote>();

        private static void Main(string[] args)
        {
            baseVotumDevice.ClickButtonsRemotes += BaseVotumDevice_ClickButtonsRemotes;

            RunWork();

            Console.ReadKey();
        }

        private static void LongTimeRemote(object remote)
        {
            var rem = (Remote)remote;

            SendbackCommand wait = new SendbackCommand(rem.ReceiverId, rem.RemoteId, RemoteCommand.CMD_WAIT);

            while (SyncFlag)
            {
                baseVotumDevice.SendCmdToRemote(wait);
                Thread.Sleep(100);
            }
        }

        private static void BaseVotumDevice_ClickButtonsRemotes(object sender, ButtonClickEventArgs e)
        {
            Console.WriteLine(e.Battary);

            e.SendbackCommand = new SendbackCommand(e.ReceiverId, e.RemoteId, RemoteCommand.CMD_WAIT);

            if (RemotesList.Any(x => x.RemoteId.Equals(e.RemoteId) && x.ReceiverId.Equals(e.ReceiverId)))
                return;
            else
            {
                Remote remote = new Remote() { RemoteId = e.RemoteId, ReceiverId = e.ReceiverId };

                remoteInRun = remote;

                thread.Start(remote);

                RemotesList.Add(remote);
            }
        }

        private static void RunWork()
        {
            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    SendbackCommand logo = new SendbackCommand(remoteInRun.ReceiverId, remoteInRun.RemoteId, RemoteCommand.CMD_DISPLAY_LOGO);
                    baseVotumDevice.SendCmdToRemote(logo);
                    SyncFlag = false;
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
