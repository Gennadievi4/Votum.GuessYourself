using RLib;
using RLib.Remotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Guess.Yourself
{
    public class StudentModel : INotifyPropertyChanged
    {
        private string nameOfTheStudentsTextFile;
        public string NameOfTheStudentsTextFile
        {
            get => nameOfTheStudentsTextFile;
            set
            {
                if (nameOfTheStudentsTextFile == value) return;
                nameOfTheStudentsTextFile = value;
                RaisePropertyChanged(nameof(NameOfTheStudentsTextFile));
            }
        }

        private string character;
        public string Character
        {
            get => character;
            set
            {
                if (character == value) return;
                character = value;
                RaisePropertyChanged(nameof(Character));
            }
        }
        public bool IsAccess { get; private set; } = true;
        public List<string> textString { get; set; } = new List<string>();

        private object txtString;
        public object TextString
        {
            get => txtString;
            set
            {
                if (txtString == value) return;
                txtString = value;
                RaisePropertyChanged(nameof(TextString));
            }
        }
        public TRemotePacket remotePacket { get; set; } = new TRemotePacket();
        //THIDMessageID

        //SendbackCommand sendback = new SendbackCommand(int RemoteCommand.CMD_DISPLAY_LOGO);
        private DateTime? StopWatch;
        //DateTime start = DateTime.Now;
        //public DateTime? Time => StopWatch;
        public DateTime? Time
        {
            get => StopWatch;
            set
            {
                if (StopWatch == value) return;
                StopWatch = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        //public string TimeStr => StopWatch.Hour == 0 ? 
        //    StopWatch.Equals(new DateTime()) ? 
        //    string.Empty : 
        //    StopWatch.ToString("mm : ss") : 
        //    StopWatch.ToString("hh : mm : ss");


        //public string RatingStr
        //{
        //    set
        //    {
        //        if (rating == default)
        //            rating.ToString();
        //    }
        //}
        private int? rating;
        public int? Rating
        {
            set
            {
                if (rating == value) return;
                rating = value;
                RaisePropertyChanged(nameof(Rating));
            }
            get
            {
                //return rating;
                if (rating == 0) return null;
                else return rating;
            }
        }

        private string question;
        public string Question
        {
            get => question;
            set
            {
                if (question == value) return;
                question = value;
                RaisePropertyChanged(nameof(Question));
            }
        }

        private int receiverId;
        public int ReceiverId
        {
            get => receiverId;
            set
            {
                if (receiverId == value) return;
                receiverId = value;
            }
        }
        private ushort? remoteId;
        public ushort? RemoteId
        {
            get => remoteId;
            set
            {
                if (remoteId == value) return;
                remoteId = value;
                RaisePropertyChanged(nameof(RemoteId));
            }
        }

        #region Цвета
        public ObservableCollection<string> Questions { get; set; } = new ObservableCollection<string>();

        public static SolidColorBrush defaultColor = new SolidColorBrush(Colors.Black);
        public SolidColorBrush AnswerColor { get; set; } = defaultColor;
        public SolidColorBrush Yes { get; set; } = defaultColor;
        public SolidColorBrush No { get; set; } = defaultColor;
        public SolidColorBrush DontKnow { get; set; } = defaultColor;

        private AnswerType userAnswerYes;
        public AnswerType UserAnswerYes
        {
            get => userAnswerYes;
            set
            {
                userAnswerYes = value;
                Yes = ChangeColor(value);
                
                RaisePropertyChanged(nameof(Yes));
            }
        }

        private AnswerType userAnswerNo;
        public AnswerType UserAnswerNo
        {
            get => userAnswerNo;
            set
            {
                userAnswerNo = value;
                No = ChangeColor(value);

                RaisePropertyChanged(nameof(No));
            }
        }

        private AnswerType userAnswerDontKnow;
        public AnswerType UserAnswerDontKnow
        {
            get => userAnswerDontKnow;
            set
            {
                userAnswerDontKnow = value;
                DontKnow = ChangeColor(value);

                RaisePropertyChanged(nameof(DontKnow));
            }
        }

        private AnswerType userAnswer;
        public AnswerType UserAnswer
        {
            get => userAnswer;
            set
            {
                userAnswer = value;
                AnswerColor = ChangeColor(value);

                RaisePropertyChanged(nameof(AnswerColor));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentModel(int remoteId, int receiverId)
        {
            RemoteId = (ushort)remoteId;
            ReceiverId = receiverId;
        }
        public StudentModel()
        {

        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void QuestionsAdd(string text)
        {
            Questions.Add($"{Questions.Count + 1}. {text}");
        }
        public void UpTime()
        {
            //TimeSpan time = (DateTime.Now - start).Duration();
            //long? dt = DateTime.Now.Ticks - StopWatch?.Ticks;
            //DateTime? now = DateTime.Now;
            //TimeSpan? elapsed = new TimeSpan(0, 0, 1);
            if (StopWatch is null) StopWatch = new DateTime();
            else StopWatch = ((DateTime)StopWatch).AddSeconds(1);
            RaisePropertyChanged(nameof(Time));
        }
        SolidColorBrush ChangeColor(AnswerType answer)
        {
            switch (answer)
            {
                case AnswerType.NotGuessed:
                    return new SolidColorBrush(Colors.Transparent);
                case AnswerType.Correct:
                    return new SolidColorBrush(Colors.Green);
                case AnswerType.NotCorrect:
                    return new SolidColorBrush(Colors.Red);
                case AnswerType.DontKnow:
                    return new SolidColorBrush(Colors.Yellow);
                case AnswerType.NotSet:
                    return defaultColor;
                default:
                    throw new Exception("Не выбран цвет!");
            }
        }
        public enum AnswerType
        {
            Correct, NotCorrect, DontKnow, NotSet, NotGuessed
        }
    }
}
