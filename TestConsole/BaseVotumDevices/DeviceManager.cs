using RLib;
using RLib.Remotes;
using System;
using System.Diagnostics;

namespace TestConsole.BaseVotumDevices
{
    public class DeviceManager
    {
        private readonly Settings settings = new Settings();

        public VotumDevicesManager VotumDevicesManager { get; private set; }

        public DeviceManager()
        {
            RunVotumDeviceSettings();
        }

        private void RunVotumDeviceSettings()
        {
            settings.InteractiveRemotesSettings.AutoT2 = true;
            settings.InteractiveRemotesSettings.InteractiveMode = true;
            settings.InteractiveRemotesSettings.RemoteShowPressedButton = false;

            VotumDevicesManager = new VotumDevicesManager(settings);
            VotumDevicesManager.Start();
        }
    }
}