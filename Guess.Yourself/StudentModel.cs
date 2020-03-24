using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace Guess.Yourself
{
    public class StudentModel : INotifyPropertyChanged
    {
        public string Character { get; set; }
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

        public int remoteId;
        public int RemoteId
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
        public SolidColorBrush AnswerColor { get; set; }

        AnswerType userAnswer;
        public AnswerType UserAnswer
        {
            get => userAnswer;
            set
            {
                userAnswer = value;
                AnswerColor = ChangeColor(value);
                RaisePropertyChanged(nameof(AnswerType));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public StudentModel(int remoteId, int receiverId)
        {
            RemoteId = remoteId;
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
                default:
                    return new SolidColorBrush(Colors.Transparent);
            }
        }
        public enum AnswerType
        {
            Correct, NotCorrect, DontKnow
        }
    }
}
