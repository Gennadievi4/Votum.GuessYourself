using RLib;
using RLib.Remotes;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Guess.Yourself
{
    public class MainWindowViewModel
    {
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();

        private readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

        public DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        DataGrid str = (DataGrid)App.Current.MainWindow.FindName("TableName");

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

        #region Private Methods
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnTick?.Invoke();
            foreach (var student in Students.Where(x => x.RemoteId != null))
            {
                //if(Students.Any(x => OnTick == x.UpTime))
                InitializeRating(student.RemoteId);
            }
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

        private void ChangeTextColorToDefault()
        {
            foreach (var a in Students.Where(x => x.UserAnswer == StudentModel.AnswerType.NotGuessed))
            {
                a.UserAnswer = StudentModel.AnswerType.NotSet;
            }
        }

        private void ResetCollection()
        {
            foreach (var std in Students)
            {
                std.Question = default;
                std.Rating = default;
                std.RemoteId = default;
                std.Time = default;
            }

        }
        private void StopTimer()
        {
            foreach (var std in Students.Where(x => x.RemoteId != default && x.ReceiverId != default))
            {
                if (OnTick != null)
                    OnTick -= std.UpTime;
            }
        }
        private void EnsureRemoteAdded(int RemoteId, int ReceiverId)
        {
            if (!Students.Any(x => x.ReceiverId.Equals(ReceiverId) && x.RemoteId.Equals(Convert.ToUInt16(RemoteId))))
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    var std = Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)) || x.RemoteId == default);
                    if (std != null)
                    {
                        std.UserAnswer = StudentModel.AnswerType.NotGuessed;
                        std.RemoteId = (ushort)RemoteId;
                        std.ReceiverId = ReceiverId;
                    }
                }));
            }
        }

        private void InitializeRating(ushort? RemoteId)
        {
            var orderedCollection = Students.OrderBy(x => x.Time);
            var ratedStudent = orderedCollection.Where(x => x.RemoteId != null).Single(x => x.RemoteId == RemoteId);
            ratedStudent.Rating = orderedCollection.Where(x => x.RemoteId != null && x.Time != null).ToList().IndexOf(ratedStudent) + 1;
            CommandManager.InvalidateRequerySuggested();
            //var std = Students.OrderBy(x => x.Time).ToList().FindIndex(x => x.Time != null);
            //var order = 1;
            //foreach (var student in Students.OrderBy(x => x.Time).Where(x => x.Time != null))
            //    student.Rating = order++;
        }

        private void GettingAQuestionsRemotely(int RemoteId, ButtonClickEventArgs e)
        {
            if (e.IsT2TextPresent && e.Button.Type == ButtonType.PauseT2)
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)))?.QuestionsAdd(e.T2Text);
                    var std = Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)));
                    if (std != null)
                    {
                        //std.UserAnswer = StudentModel.AnswerType.NotSet;
                        std.Question = e.T2Text;
                        OnTick -= std.UpTime;
                        //std.send = e;
                        // std.UserAnswer = StudentModel.AnswerType.NotGuessed;
                    }
                }));
            }
        }
        #endregion

        #region Команды
        public RelayCommand<StudentModel> yesCmd = null;

        public RelayCommand<StudentModel> YesCmd => yesCmd ?? (yesCmd = new RelayCommand<StudentModel>((param) =>
        {
            //param.UserAnswer = StudentModel.AnswerType.Correct;
            //param.remotePacket.RemoteID = (int)param.RemoteId;
            //param.remotePacket.RemoteCommand = TRemoteCommandID.RF_ACK_DISPLAY_LOGO;
            SendbackCommand send = new SendbackCommand(param.ReceiverId, (int)param.RemoteId, RemoteCommand.CMD_DISPLAY_LOGO);
            param.remotePacket.ComplectID = param.ReceiverId;
            param.remotePacket.RemoteID = (int)param.RemoteId;
            param.remotePacket.PacketLength = 6;
            param.remotePacket.RemoteCommand = TRemoteCommandID.RF_ACK_DISPLAY_LOGO;

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
            //param.UserAnswer = StudentM1odel.AnswerType.NotCorrect;
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
            //param.UserAnswer = StudentModel.AnswerType.DontKnow;
            param.Question = null;
            if (OnTick != param.UpTime)
            {
                OnTick += param.UpTime;
            }
        },
        (param) =>
        {
            return (param != null) ? !string.IsNullOrEmpty(param.Question) : false;
        }));

        public RelayCommand<StudentModel> questionCmd = null;
        public RelayCommand<StudentModel> QuestionCmd => questionCmd ?? (questionCmd = new RelayCommand<StudentModel>((param) =>
        {
            new QuestionView(param.Questions).ShowDialog();
        },
            (param) =>
            {
                return (param != null && param.Questions.Count == 0) ? false : true;
            }
            ));

        public RelayCommand<StudentModel> closeApp = null;
        public RelayCommand<StudentModel> CloseApp => closeApp ?? (closeApp = new RelayCommand<StudentModel>((param) =>
        {
            Environment.Exit(0);
        }));

        public RelayCommand<StudentModel> endGame = null;
        public RelayCommand<StudentModel> EndGame => endGame ?? (endGame = new RelayCommand<StudentModel>((param) =>
        {
            ChangeTextColorToDefault();
            StopTimer();

            str.IsEnabled = false;
            deviceManager.votumManager.Stop();
        },
            (param) =>
            {
                var x = str.Columns[7].GetCellContent(str.Items[0]) as TextBlock;
                return str.IsEnabled && x != null ? x.Text != "" : false;
            }));

        public RelayCommand<StudentModel> resetGame = null;
        public RelayCommand<StudentModel> ResetGame => resetGame ?? (resetGame = new RelayCommand<StudentModel>((param) =>
        {
            ResetCollection();

            str.IsEnabled = true;
            deviceManager.votumManager.Start();
        },
            (param) =>
            {
                return (param != null || !str.IsEnabled) ? true : false;
            }
            ));
        #endregion
    }
}
