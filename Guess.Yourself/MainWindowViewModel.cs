using Guess.Yourself.Interfaces;
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
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();
        public AsyncObservableCollection<StudentWinner> Winners { get; set; } = new AsyncObservableCollection<StudentWinner>();
        public static IFileService FileService { get; private set; }

        private string idRemouteStdWinner;
        public string IdRemoteStdWinner
        {
            get => idRemouteStdWinner;
            set
            {
                if (idRemouteStdWinner == value) return;
                idRemouteStdWinner = value;
                OnPropertyChanged();
            }
        }

        private bool isAnimation;
        public bool IsAnimation
        {
            get => isAnimation;
            set
            {
                if (isAnimation == value) return;
                isAnimation = value;
                OnPropertyChanged();
            }
        }

        private bool isAnimationEndGame;
        public bool IsAnimationEndGame
        {
            get => isAnimationEndGame;
            set
            {
                if (isAnimationEndGame == value) return;
                isAnimationEndGame = value;
                OnPropertyChanged();
            }
        }
        public static IDialogServices DialogService { get; private set; }

        private StudentModel studentModel;
        public StudentModel SelectedStudent
        {
            get => studentModel;
            set
            {
                if (studentModel == value) return;
                studentModel = value;
            }
        }



        private readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

        public DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        DataGrid str = (DataGrid)App.Current.MainWindow.FindName("TableName");

        public event Action OnTick;
        public MainWindowViewModel() { }
        public MainWindowViewModel(IDialogServices dialogService, IFileService fileService)
        {
            //this.win = win;
            DialogService = dialogService;
            FileService = fileService;

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
            if (Winners.Any(x => x.StdWinner.RemoteId == e.RemoteId))
            {
                return;
            }
            else
            {
                EnsureRemoteAdded(e.RemoteId, e.ReceiverId);
                GettingAQuestionsRemotely(e.RemoteId, e);
                IsAnimation = false;
                FindWinner(e.RemoteId, e);
            }
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
            foreach (var a in Students.Where(x => x.UserAnswer == AnswerType.NotGuessed))
            {
                a.UserAnswer = AnswerType.NotSet;
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
                std.Character = default;
                std.IsAccess = true;
                std.IsWinner = false;
                std.Questions.Clear();
            }
            IsAnimation = false;
            IsAnimationEndGame = false;
            IdRemoteStdWinner = default;
            Winners.Clear();
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
                    //var std = Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)) || x.RemoteId == default);
                    var std = Students.FirstOrDefault(x => !string.IsNullOrEmpty(x.Character) && x.RemoteId == default);
                    if (std != null)
                    {
                        std.UserAnswer = AnswerType.NotGuessed;
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

        private void FindWinner(int RemouteId, ButtonClickEventArgs e)
        {
            if (e.IsT2TextPresent && e.Button.Type == ButtonType.PauseT2)
            {
                var studentWinnerListBox = Students
                    .Where(x => x.RemoteId != null && x.Question != null && !string.IsNullOrWhiteSpace(x.Character))
                    .Select((std, index) => (index, std))
                    .Where(x => x.std.RemoteId == RemouteId && x.std.Question.Contains(x.std?.Character));
                //.Select(x => x.index);

                var student = Students.Where(x => x.RemoteId.Equals(Convert.ToUInt16(RemouteId)) && !string.IsNullOrWhiteSpace(x.Character) && x.Question.Contains(x.Character));
                student.ToList().ForEach(x => { x.IsWinner = true; x.IsAccess = false; });

                int temp = 0;
                foreach (var std in studentWinnerListBox)
                {
                    if (!string.IsNullOrWhiteSpace(std.std.Character))
                    {
                        temp += std.index + 1;
                        IsAnimation = true;
                        Winners.Add(new StudentWinner { StdWin = temp, StdWinner = std.std });
                        IdRemoteStdWinner = $"Внимание! Участник №{temp} угадал!";
                    }
                }
                //App.Current.Dispatcher.Invoke(new Action(() =>
                //{
                //    int temp = 0;
                //    foreach (var std in studentWinnerListBox)
                //    {
                //        temp += std.index + 1;
                //        Winners.Add(new StudentWinner { StdWin = temp, StdWinner = std.std });
                //    }
                //}));
            }
        }

        private void GettingAQuestionsRemotely(int RemoteId, ButtonClickEventArgs e)
        {
            if (e.IsT2TextPresent && e.Button.Type == ButtonType.PauseT2)
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)))?.QuestionsAdd(e.T2Text, AnswerType.NotGuessed);
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

        public ICommand YesCmd => yesCmd ?? (yesCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.Questions.First(x => x.Question.Contains(param.Question) && x.UserAnswer == AnswerType.NotGuessed)
            .UserAnswer = AnswerType.Correct;


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
        public ICommand NoCmd => noCmd ?? (noCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.Questions
            .First(x => x.Question.Contains(param.Question) && x.UserAnswer == AnswerType.NotGuessed)
            .UserAnswer = AnswerType.NotCorrect;

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
        public ICommand DontKnowCmd => dontKnowCmd ?? (dontKnowCmd = new RelayCommand<StudentModel>((param) =>
        {
            param.Questions.First(x => x.Question.Contains(param.Question) && x.UserAnswer == AnswerType.NotGuessed)
            .UserAnswer = AnswerType.DontKnow;

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
        public ICommand QuestionCmd => questionCmd ?? (questionCmd = new RelayCommand<StudentModel>((param) =>
        {
            new QuestionView() { DataContext = param }.ShowDialog();
        },
            (param) =>
            {
                return (param != null && param.Questions.Count > 0);
            }
            ));

        public RelayCommand<StudentModel> closeApp = null;
        public ICommand CloseApp => closeApp ?? (closeApp = new RelayCommand<StudentModel>((param) =>
        {
            Environment.Exit(0);
        }));

        public RelayCommand<StudentModel> endGame = null;
        public ICommand EndGame => endGame ?? (endGame = new RelayCommand<StudentModel>((param) =>
        {
            ChangeTextColorToDefault();
            StopTimer();

            Students.Where(x => x.Question == null && x.RemoteId != null).ToList().ForEach(x => { x.Rating = null; x.Time = null; });

            if (Students.Any(x => x.Question != null && x.RemoteId != null && x.Character != null && x.Rating != null && x.Question.Equals(x.Character)))
            {
                var winner = Students
                .Where(x => x.Question != null && x.RemoteId != null && x.Rating != null)
                .OrderBy(x => x.Rating)
                .ToList()
                .Single().RemoteId;
                IdRemoteStdWinner = $"Победил участник игры с пультом №{winner}";
            }
            else
            {
                if (Winners.Any())
                {
                    IsAnimationEndGame = true;
                    IdRemoteStdWinner = $"Победил участник игры с пультом №{Winners.Select((std, ind) => (ind, std)).OrderBy(x => x.ind).Single(x => x.ind == 0).std.StdWinner.RemoteId}";
                }
                else
                {
                    IsAnimationEndGame = true;
                    IdRemoteStdWinner = $"В игре нет победителей!";
                }
            }

            str.IsEnabled = false;
            deviceManager.votumManager.Stop();
        },
            (param) =>
            {
                //var x = str.Columns[7].GetCellContent(str.Items[0]) as System.Windows.Controls.Button;
                //return str.IsEnabled && x != null ? x.Content.ToString() != "" : false;
                return Students.Any(x => x.Question != null || x.Time != null) && str.IsEnabled;
            }));

        public RelayCommand<StudentModel> resetGame = null;
        public ICommand ResetGame => resetGame ?? (resetGame = new RelayCommand<StudentModel>((param) =>
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

        public RelayCommand<StudentModel> openAboutUs = null;
        public ICommand OpenAboutUs => openAboutUs ?? (openAboutUs = new RelayCommand<StudentModel>((std) =>
            {
                new AboutGuessYourself().ShowDialog();
            }));
        public RelayCommand<StudentWinner> winnersOpen = null;
        public ICommand WinnersOpen => winnersOpen ?? (winnersOpen = new RelayCommand<StudentWinner>((param) =>
        {
            //new WinnersView() { DataContext = this }.ShowDialog();
            WinnersViewModel wstd = new WinnersViewModel(this, new WinnersView());
            wstd.Winners.ShowDialog();
        },
            (param) =>
            {
                return Winners.Count != 0;
            }
            ));
        #endregion
    }
}