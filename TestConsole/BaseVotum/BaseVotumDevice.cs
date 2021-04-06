using RLib;
using System;

namespace TestConsole.BaseVotum
{
    public class BaseVotumDevice
    {
        private readonly VotumDevicesManager votumDevicesManager;

        public event EventHandler<ButtonClickEventArgs> ClickButtonsRemotes
        {
            add { votumDevicesManager.ButtonClicked += value; }
            remove { votumDevicesManager.ButtonClicked -= value; }
        }

        public BaseVotumDevice()
        {
            votumDevicesManager = InitVotumDevice();
        }

        private VotumDevicesManager InitVotumDevice()
        {
            VotumDevicesManager vDM = new VotumDevicesManager();
            vDM.Settings.InteractiveRemotesSettings.AutoT2 = true;
            vDM.Settings.InteractiveRemotesSettings.InteractiveMode = true;

            vDM.Start();

            return vDM;
        }

        public void SendCmdToRemote(SendbackCommand cmd)
        {
            votumDevicesManager.SendCommandToRemote(cmd);
        }
    }
}