using RLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Guess.Yourself
{
    public class MainWindowViewModel
    {
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();

        private readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

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
            EnsureRemoteAdded(e.RemoteId, e.ReceiverId);
            GettingAQuestionsRemotely(e.RemoteId, e);
            //var t2 = e.Button;
        }

        private void EnsureRemoteAdded(int RemoteId, int ReceiverId)
        {
            if (!Students.Any(x => x.ReceiverId.Equals(ReceiverId) && x.RemoteId.Equals(RemoteId)))
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var std = Students.FirstOrDefault(x => x.RemoteId.Equals(RemoteId) || x.RemoteId == 0);
                    if (std != null)
                        std.RemoteId = RemoteId;
                }));
            }
        }

        private void GettingAQuestionsRemotely(int RemoteId, ButtonClickEventArgs e)
        {
            if (e.IsT2TextPresent && e.Button.Type == ButtonType.PauseT2)
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Students.FirstOrDefault(x => x.remoteId.Equals(RemoteId))?.QuestionsAdd(e.T2Text);
                    var std = Students.FirstOrDefault(x => x.remoteId.Equals(RemoteId));
                    if (std != null)
                        std.Question = e.T2Text;
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
