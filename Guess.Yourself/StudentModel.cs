using RLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Guess.Yourself
{
    public class StudentModel : INotifyPropertyChanged
    {
        public string Character { get; set; }
        public bool IsAccess { get; private set; } = true;
        public ButtonClickEventArgs send { get; set; }
        DateTime StopWatch;
        public string Time => StopWatch.Hour == 0 ? 
            StopWatch.Equals(new DateTime()) ? 
            string.Empty : 
            StopWatch.ToString("mm : ss") : 
            StopWatch.ToString("hh : mm : ss");
        public string rating;
        public string Rating
        {
            get => rating;
            set
            {
                if (rating == value) return;
                rating = value;
                RaisePropertyChanged(nameof(Rating));
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

        public string remoteId;
        public string RemoteId
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
            RemoteId = remoteId.ToString();
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
            StopWatch = StopWatch.AddSeconds(1);
            RaisePropertyChanged(nameof(Time));
        }
        SolidColorBrush ChangeColor(AnswerType answer)
        {
            switch (answer)
            {
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
            Correct, NotCorrect, DontKnow, NotSet
        }
    }
}
