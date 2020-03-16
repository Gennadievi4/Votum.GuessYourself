using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guess.Yourself
{
    class TheObjectOfTheStudent //: INotifyPropertyChanged
    {
        public string Character { get; set; }
        public string Question { get; set; }

        public TheObjectOfTheStudent()
        {

        }

        //    public Color AnswerColor { get; set; } // свойство на привязку

        //    AnswerType userAnswer;
        //    public AnswerType userAnswer
        //    {
        //        get => userAnswer;
        //        set =>{
        //            _ = value ?? throw new Exception("there cant be null value");

        //            userAnswer = value;
        //            AnswerColor(ChangeColor(value));
        //            RaisePropertyChanged("AnswerColor");
        //        }
        //    }



        //    Color ChangeColor(AnswerType answer) =>

        //        answer switch   // switch expression позырь я их ещё не изучал
        //        {
        //            Correct => new Color("Green"),
        //            NotCorrect => new Color("Red"),
        //            DontKnow => new Color("Yellow")
        //        };
        //}

        //public enum AnswerType // хз
        //{
        //    Correct, NotCorrect, DontKnow
    }
}
