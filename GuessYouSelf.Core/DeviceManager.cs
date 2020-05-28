using RLib;
using System;

namespace GuessYouSelf.Core
{
    public class DeviceManager
    {
        private readonly UsbWatcher usbWatcher = new UsbWatcher();
        public VotumDevicesManager votumManager { get; private set; }
        public DeviceManager(VotumDevicesManager manager)
        {
            votumManager = manager;
            InitVotumDevice();
        }

        public void InitVotumDevice()
        {
            votumManager.Settings.InteractiveRemotesSettings.InteractiveMode = true;
            votumManager.Settings.InteractiveRemotesSettings.AutoT2 = true;
            usbWatcher.DeviceInserted += Watcher_DeviceInserted;
            usbWatcher.DeviceRemoved -= Watcher_DeviceInserted;
            votumManager.Start();
        }

        private void Restart()
        {
            if (votumManager != null)
            {
                votumManager.Stop();
            }
            votumManager.Start();
        }

        private void Watcher_DeviceInserted(object sender, EventArgs e)
        {
            Restart();
        }
    }
}