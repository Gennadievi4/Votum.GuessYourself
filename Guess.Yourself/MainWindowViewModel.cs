using RLib;
using RLib.Remotes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Guess.Yourself
{
    public class MainWindowViewModel
    {
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();

        private readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

        public DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        public event Action OnTick;
        public StudentModel SelectedStudent { get; set; }

        public MainWindowViewModel()
        {
            for (int i = 0; i < 32; i++)
            {
                Students.Add(new StudentModel());
            }
            timer.Tick += DispatcherTimer_Tick;
            timer.Start();
            deviceManager.votumManager.ButtonClicked += VotumManager_ButtonClicked;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnTick?.Invoke();
        }

        private void VotumManager_ButtonClicked(object sender, ButtonClickEventArgs e)
        {
            EnsureRemoteAdded(e.RemoteId, e.ReceiverId);
            GettingAQuestionsRemotely(e.RemoteId, e);
            //if (e.Button.Type == ButtonType.PauseT2)
            //{
            //    RemoteCommand remoteCMD = RemoteCommand.CMD_NO_ACTION;
            //    SendbackCommand cmd = new SendbackCommand(e.ReceiverId, e.RemoteId, RemoteCommand.CMD_NO_ACTION);
            //    e.SendbackCommand = SendbackCommand.Led(true);
            //    e.SendbackCommand = new SendbackCommand(e.ReceiverId, e.RemoteId, RemoteCommand.CMD_NO_ACTION);
            //}
            //var t2 = e.Button;
        }

        private void EnsureRemoteAdded(int RemoteId, int ReceiverId)
        {
            if (!Students.Any(x => x.ReceiverId.Equals(ReceiverId) && x.RemoteId.Equals(Convert.ToString(RemoteId))))
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var std = Students.FirstOrDefault(x => Convert.ToInt32(x.RemoteId).Equals(RemoteId) || Convert.ToInt32(x.RemoteId) == 0);
                    if (std != null)
                        std.RemoteId = RemoteId.ToString();
                }));
            }
        }

        private void GettingAQuestionsRemotely(int RemoteId, ButtonClickEventArgs e)
        {
            if (e.IsT2TextPresent && e.Button.Type == ButtonType.PauseT2)
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Students.FirstOrDefault(x => x.remoteId.Equals(Convert.ToString(RemoteId)))?.QuestionsAdd(e.T2Text);
                    var std = Students.FirstOrDefault(x => x.remoteId.Equals(Convert.ToString(RemoteId)));
                    if (std != null)
                    {
                        std.Question = e.T2Text;
                        OnTick -= std.UpTime;
                        std.send = e;
                        std.UserAnswer = StudentModel.AnswerType.NotSet;
                    }
                }));
            }
        }

        public RelayCommand<StudentModel> yesCmd = null;

        public RelayCommand<StudentModel> YesCmd => yesCmd ?? (yesCmd = new RelayCommand<StudentModel>((param) =>
       {
           param.UserAnswer = StudentModel.AnswerType.Correct;
           param.Question = null;
           if (OnTick != param.UpTime) 
           {
               OnTick += param.UpTime;
           }
       },
        (stdParam) =>
        {
            return (stdParam != null) ? !string.IsNullOrEmpty(stdParam.Question) : false;
        }));

        public RelayCommand<StudentModel> noCmd = null;
        public RelayCommand<StudentModel> NoCmd => noCmd ?? (noCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.UserAnswer = StudentModel.AnswerType.NotCorrect;
            param.Question = null;
            if (OnTick != param.UpTime)
            {
                OnTick += param.UpTime;
            }
        },
        (stdParam) =>
        {
            return (stdParam != null) ? !string.IsNullOrEmpty(stdParam.Question) : false;
        }));

        public RelayCommand<StudentModel> dontKnowCmd = null;
        public RelayCommand<StudentModel> DontKnowCmd => dontKnowCmd ?? (dontKnowCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.UserAnswer = StudentModel.AnswerType.DontKnow;
            param.Question = null;
            if (OnTick != param.UpTime)
            {
                OnTick += param.UpTime;
            }
            if(!param.send.IsSendbackCommandAvailable)
                param.send.SendbackCommand = SendbackCommand.DisplayStringClear("Неизвестно!");
        },
        (param) =>
        {
            return (param != null) ? !string.IsNullOrEmpty(param.Question) : false;
        }));

        public RelayCommand<StudentModel> questionCmd = null;
        public RelayCommand<StudentModel> QuestionCmd => questionCmd ?? (questionCmd = new RelayCommand<StudentModel>((param) =>
        {
            new QuestionView(param.Questions).ShowDialog();
        }));
    }
}
