using RLib;
using System;

namespace Guess.Yourself
{
    class InitializeDevice
    {
        UsbWatcher watcher;
        VotumDevicesManager devicesManager;

        public InitializeDevice()
        {
            StartStopDevice();
        }

        private void StartStopDevice()
        {
            watcher = new UsbWatcher();
            devicesManager = new VotumDevicesManager();
            devicesManager.Settings.InteractiveRemotesSettings.InteractiveMode = true;
            devicesManager.Settings.InteractiveRemotesSettings.AutoT2 = true;
            watcher.DeviceInserted += Watcher_DeviceInserted;
            watcher.DeviceRemoved += Watcher_DeviceInserted;
            devicesManager.Start();
        }

        private void InitDeviceManager()
        {
            if (devicesManager != null)
            {
                devicesManager.Stop();
            }
            devicesManager.Start();
        }

        private void Watcher_DeviceInserted(object sender, EventArgs e)
        {
            InitDeviceManager();
        }
    }
}