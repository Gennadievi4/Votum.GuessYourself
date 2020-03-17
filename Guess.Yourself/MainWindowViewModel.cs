using RLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Guess.Yourself
{
    public class MainWindowViewModel
    {
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();

        readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

        public StudentModel SelectedStudent { get; set; }

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
                Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                {
                    Students.Add(new StudentModel(ReceiverId, RemoteId));
                }));
            }
        }

        public RelayCommand<StudentModel> yesCmd = null;

        public RelayCommand<StudentModel> YesCmd => yesCmd ?? (yesCmd = new RelayCommand<StudentModel>((param) =>
       {
           param.UserAnswer = StudentModel.AnswerType.Correct;
       }));

        public RelayCommand<StudentModel> noCmd = null;
        public RelayCommand<StudentModel> NoCmd => noCmd ?? (noCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.UserAnswer = StudentModel.AnswerType.NotCorrect;
        }));

        public RelayCommand<StudentModel> dontKnowCmd = null;
        public RelayCommand<StudentModel> DontKnowCmd => dontKnowCmd ?? (dontKnowCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.UserAnswer = StudentModel.AnswerType.DontKnow;
        }));

        public RelayCommand<StudentModel> questionCmd = null;
        public RelayCommand<StudentModel> QuestionCmd => questionCmd ?? (questionCmd = new RelayCommand<StudentModel>((param) =>
        {
            new QuestionView(param.Questions).ShowDialog();
        }));
    }
}
