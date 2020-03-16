using RLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Guess.Yourself
{
    class MainWindowViewModel
    {
        ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();

        readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

        public MainWindowViewModel()
        {
            for (int i = 0; i < 32; i++)
            {
                Students.Add(new StudentModel());
            }

            deviceManager.votumManager.ButtonClicked += VotumManager_ButtonClicked;
        }

        private void VotumManager_ButtonClicked(object sender, ButtonClickEventArgs e)
        {
            EnsureRemoteAdded(e.ReceiverId, e.RemoteId);
            var newStudent = Students.FirstOrDefault(x => x.ReceiverId.Equals(e.ReceiverId) && x.RemoteId.Equals(e.RemoteId));
            _ = newStudent ?? throw new Exception("Что-то пошло не так!");
        }

        private void EnsureRemoteAdded(int ReceiverId, int RemoteId)
        {
            if (!Students.Any(x => x.ReceiverId.Equals(ReceiverId) && x.RemoteId.Equals(RemoteId)))
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Students.Add(new StudentModel(ReceiverId, RemoteId));
                });
            }
        }
    }
}
