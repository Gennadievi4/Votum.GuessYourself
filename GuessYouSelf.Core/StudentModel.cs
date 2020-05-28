using RLib;
using RLib.Remotes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace GuessYouSelf.Core
{
    public class StudentModel : INotifyPropertyChanged
    {
        public string Character { get; set; }
        public bool IsAccess { get; private set; } = true;
        public RemoteEventArgs send;
        public TRemotePacket remotePacket = new TRemotePacket();

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
        public int ReceiverId { get; set; }

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
        public List<string> Questions { get; set; } = new List<string>();

        public static SolidColorBrush defaultColor = new SolidColorBrush(Colors.Black);
        public SolidColorBrush AnswerColor { get; set; } = defaultColor;

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
            Questions.Add($"{Questions.Count + 1}. " + $" {text}");
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
                    //if (UserAnswer == AnswerType.Correct)
                    //    remotePacket.RemoteCommand = (TRemoteCommandID)RemoteCommand.CMD_DISPLAY_LOGO;
                    return new SolidColorBrush(Colors.Green);
                case AnswerType.NotCorrect:
                    //if (UserAnswer == AnswerType.NotCorrect)
                    //    SendbackCommand.DisplayStringClear("Нет!");
                    return new SolidColorBrush(Colors.Red);
                case AnswerType.DontKnow:
                    //if (UserAnswer == AnswerType.DontKnow)
                    //    SendbackCommand.DisplayStringClear("Неизвестно!");
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