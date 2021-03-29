using RLib;
using System;

namespace Guess.Yourself
{
    public class DeviceManager
    {
        private readonly UsbWatcher usbWatcher = new UsbWatcher();
        public VotumDevicesManager VotumManager { get; private set; }
        public DeviceManager(VotumDevicesManager manager)
        {
            VotumManager = manager;
            InitVotumDevice();
        }

        public void InitVotumDevice()
        {
            VotumManager.Settings.InteractiveRemotesSettings.InteractiveMode = true;
            VotumManager.Settings.InteractiveRemotesSettings.AutoT2 = true;
            usbWatcher.DeviceInserted += Watcher_DeviceInserted;
            usbWatcher.DeviceRemoved += Watcher_DeviceInserted;
            VotumManager.Start();
        }

        private void Restart()
        {
            if (VotumManager != null)
            {
                VotumManager.Stop();
            }
            VotumManager.Start();
        }

        private void Watcher_DeviceInserted(object sender, EventArgs e)
        {
            Restart();
        }
    }
}