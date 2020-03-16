using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Guess.Yourself
{
    public class StudentModel : INotifyPropertyChanged
    {
        public string Character { get; set; }
        public string Question { get; set; }
        public int ReceiverId { get; set; }
        public int RemoteId { get; set; }
        public List<string> Questions { get; set; } = new List<string> { };
        public SolidColorBrush AnswerColor { get; set; }

        AnswerType userAnswer;

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

        public enum AnswerType
        {
            Correct, NotCorrect, DontKnow
        }
    }
}
