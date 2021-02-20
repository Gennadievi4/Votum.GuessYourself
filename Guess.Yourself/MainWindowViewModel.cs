using Guess.Yourself.Interfaces;
using RLib;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Guess.Yourself
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Private Fields
        private int index;
        private string _TotalNumberOfStudents;
        private string idRemouteStdWinner;
        private bool isAnimation = false;
        private bool isAnimationEndGame;
        private event Action OnTick;
        private StudentModel studentModel;
        #endregion

        #region Public Properties
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();
        public AsyncObservableCollection<StudentWinner> Winners { get; set; } = new AsyncObservableCollection<StudentWinner>();
        public static IFileService FileService { get; private set; }
        public static IDialogServices DialogService { get; private set; }

        public string TotalNumberOfStudents
        {
            get => _TotalNumberOfStudents;
            set
            {
                if (_TotalNumberOfStudents == value) return;
                _TotalNumberOfStudents = value;
                OnPropertyChanged(nameof(TotalNumberOfStudents));
            }
        }

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
        public StudentModel SelectedStudent
        {
            get => studentModel;
            set
            {
                if (studentModel == value) return;
                studentModel = value;
            }
        }
        #endregion

        private readonly DeviceManager deviceManager = new DeviceManager(new VotumDevicesManager());

        public DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        DataGrid str = (DataGrid)App.Current.MainWindow.FindName("TableName");
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
            deviceManager.VotumManager.ButtonClicked += VotumManager_ButtonClicked;
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
            //if (e.T2Text != null && !Students.Any(x => string.Equals(x.Character, e.T2Text, StringComparison.OrdinalIgnoreCase)))
            //{
            //    e.SendbackCommand = SendbackCommand.DisplayStringClear("Введена английская буква! Повторите ввод!");
            //    return;
            //}
            //bool es = e.IsSendbackCommandPresent;

            //if (e.IsSendbackCommandAvailable && e.SendbackCommand?.Packet.RemoteCommand != TRemoteCommandID.RF_ACK_WAIT)
            //    e.SendbackCommand = new SendbackCommand(e.ReceiverId, e.RemoteId, RemoteCommand.CMD_WAIT);

            if (Winners.Any(x => x.StdWinner.RemoteId == e.RemoteId))
            {
                e.SendbackCommand = SendbackCommand.DisplayStringClear("Вы угадали! Ожидайте остальных!");
                return;
            }
            else
            {
                EnsureRemoteAdded(e.RemoteId, e.ReceiverId);
                GettingAQuestionsRemotely(e.RemoteId, e);
                //IsAnimation = false;
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

        private void GetTotalSumStudents(int RemoteId)
        {
            TotalNumberOfStudents = $"{Students.Count(std => std.RemoteId != null)} общее число участников";
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
                std.NumberStudent = default;
                std.Questions.Clear();
            }
            index = default;
            IsAnimation = true;
            IsAnimationEndGame = false;
            IdRemoteStdWinner = default;
            TotalNumberOfStudents = "Участников игры нет!";
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
                        std.NumberStudent = ++index;
                        std.RemoteId = (ushort)RemoteId;
                        std.ReceiverId = ReceiverId;
                        GetTotalSumStudents(RemoteId);
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

        public void BlockWinnerCell(StudentModel student)
        {
            var block_winner = Students.FirstOrDefault(std => std.IsWinner == true && std.RemoteId.Equals(student.RemoteId));
            if (block_winner != null)
            {
                block_winner.IsAccess = false;
            }
        }

        public void BlinWinnerkCell(int RemoteId)
        {
            //var student = Students.Where(x => x.RemoteId.Equals(Convert.ToUInt16(RemouteId)) && !string.IsNullOrWhiteSpace(x.Character) && x.Question.Contains(x.Character));
            var student = Students.Where(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)) && !string.IsNullOrWhiteSpace(x.Character) && string.Equals(x.Character, x.Question, StringComparison.OrdinalIgnoreCase));
            student.ToList().ForEach(x => { x.IsWinner = true; });
        }

        private void FindWinner(int RemouteId, ButtonClickEventArgs e)
        {
            if (e.IsT2TextPresent && e.Button.Type == ButtonType.PauseT2)
            {
                var studentWinnerListBox = Students
                    .Where(x => x.RemoteId != null && x.Question != null && !string.IsNullOrWhiteSpace(x.Character))
                    //.Select((std, index) => (index, std))
                    //.Select((std, index) => new { index, std })
                    //.Where(x => x.std.RemoteId == RemouteId && x.std.Question.Contains(x.std?.Character));
                    //.Select(x => x.index);
                    //.Where(x => x.RemoteId == RemouteId && x.Question.Contains(x.Character));
                    .Where(x => x.RemoteId == RemouteId && string.Equals(x.Character, x.Question, StringComparison.OrdinalIgnoreCase));

                BlinWinnerkCell(RemouteId);

                //int temp = 0;

                foreach (var std in studentWinnerListBox)
                {
                    if (!string.IsNullOrWhiteSpace(std.Character))
                    {
                        //temp += std.index + 1;
                        IsAnimation = true;
                        Winners.Add(new StudentWinner { StdWin = std.NumberStudent, StdWinner = std, TextWinnersTable = $"Участник №{std.NumberStudent} угадал объект {std.Character}" });
                        IdRemoteStdWinner = $"Внимание! Участник №{std.NumberStudent} угадал!";
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
                    //Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)))?.QuestionsAdd(e.T2Text, AnswerType.NotGuessed);
                    var std = Students.FirstOrDefault(x => x.RemoteId.Equals(Convert.ToUInt16(RemoteId)) && !string.IsNullOrWhiteSpace(x.Character) && string.IsNullOrWhiteSpace(x.Question));
                    if (std != null)
                    {
                        //std.UserAnswer = StudentModel.AnswerType.NotSet;
                        std.Question = e.T2Text;
                        std.UserAnswer = AnswerType.NotGuessed;
                        OnTick -= std.UpTime;
                        //std.send = e;
                        // std.UserAnswer = StudentModel.AnswerType.NotGuessed;
                    }
                }));
            }
        }
        #endregion

        #region Commands
        public RelayCommand<StudentModel> yesCmd = null;

        public ICommand YesCmd => yesCmd ?? (yesCmd = new RelayCommand<StudentModel>((param) =>
        {
            Students
            .FirstOrDefault(std => std.RemoteId.Equals(param.RemoteId))
            .QuestionsAdd(param.Question, AnswerType.NotGuessed);

            param.TotalNumberQuestions = Students.FirstOrDefault(std => std.RemoteId.Equals(param.RemoteId)).Questions.Count();

            param.Questions
            .First(x => x.Question.Contains(param.Question) && x.UserAnswer == AnswerType.NotGuessed)
            .UserAnswer = AnswerType.Correct;

            if (OnTick != param.UpTime && !param.IsWinner)
            {
                param.Question = null;
                OnTick += param.UpTime;
            }

            BlockWinnerCell(param);

            //param.remotePacket.RemoteID = (int)param.RemoteId;
            //param.remotePacket.RemoteCommand = TRemoteCommandID.RF_ACK_DISPLAY_LOGO;

        },
        (stdParam) =>
        {
            //return (stdParam != null) ? !string.IsNullOrEmpty(stdParam.Question) : false;
            return stdParam != null && !string.IsNullOrEmpty(stdParam.Question);
        }));

        public RelayCommand<StudentModel> noCmd = null;
        public ICommand NoCmd => noCmd ?? (noCmd = new RelayCommand<StudentModel>((param) =>
        {
            Students
            .FirstOrDefault(std => std.RemoteId.Equals(param.RemoteId))
            .QuestionsAdd(param.Question, AnswerType.NotGuessed);

            param.TotalNumberQuestions = Students
            .FirstOrDefault(std => std.RemoteId.Equals(param.RemoteId))
                .Questions
                .Count();

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
            return (stdParam != null) && !string.IsNullOrEmpty(stdParam.Question);
        }));

        public RelayCommand<StudentModel> dontKnowCmd = null;
        public ICommand DontKnowCmd => dontKnowCmd ?? (dontKnowCmd = new RelayCommand<StudentModel>((param) =>
        {
            Students
            .FirstOrDefault(std => std.RemoteId.Equals(param.RemoteId))
            .QuestionsAdd(param.Question, AnswerType.NotGuessed);

            param.TotalNumberQuestions = Students
            .FirstOrDefault(std => std.RemoteId.Equals(param.RemoteId))
                .Questions
                .Count();

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
                return param != null && param.Questions.Count > 0;
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

            Students.Where(x => x.Rating != null && x.Time != null && x.RemoteId != null && !string.Equals(x.Character, x.Question, StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(x => { x.Rating = null; x.Time = null; });

            //if (Students.Any(x => x.Question != null && x.RemoteId != null && x.Character != null && x.Rating != null && x.Question.Equals(x.Character)))
            //{
            //    var winner = Students
            //    .Where(x => x.Question != null && x.RemoteId != null && x.Rating != null)
            //    .OrderBy(x => x.Rating)
            //    .First().RemoteId;
            //    IdRemoteStdWinner = $"Победил участник игры №{Winners.Select((std, ind) => (ind, std)).OrderBy(x => x.ind).Single(x => x.ind == 0).std.StdWinner.NumberStudent} с пультом №{winner}";
            //}
            //else
            //{
            if (Winners.Any())
            {
                IsAnimationEndGame = true;
                IdRemoteStdWinner = $"Победил участник игры №{Winners.Select((std, ind) => (ind, std)).OrderBy(x => x.ind).Single(x => x.ind == 0).std.StdWinner.NumberStudent} с пультом №{Winners.Select((std, ind) => (ind, std)).OrderBy(x => x.ind).Single(x => x.ind == 0).std.StdWinner.RemoteId}";
            }
            else
            {
                IsAnimationEndGame = true;
                IdRemoteStdWinner = $"В игре нет победителей!";
            }
            //}

            str.IsEnabled = false;
            deviceManager.VotumManager.Stop();
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
            deviceManager.VotumManager.Start();
        },
            (param) =>
            {
                return param != null || !str.IsEnabled;
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

        public RelayCommand<object> moveFocusDownWithEnter = null;
        public ICommand MoveFocusDownWithEnter => moveFocusDownWithEnter ?? (moveFocusDownWithEnter = new RelayCommand<object>((param) =>
        {
            if (!(param is FrameworkElement element)) return;
            var root = element.FindVisualRoot();
            FocusManager.SetFocusedElement(root, element);
        },
            (param) =>
            {
                return param is FrameworkElement;
            }));
        #endregion
    }
}