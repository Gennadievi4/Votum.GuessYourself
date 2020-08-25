namespace Guess.Yourself
{
    public class StudentWinner : NotifyPropertyChanged
    {
        private int? stdWin;
        public int? StdWin
        {
            get => stdWin;
            set
            {
                if (stdWin == value) return;
                stdWin = value;
                OnPropertyChanged();
            }
        }

        //private int? idRemouteStdWinner;
        //public int? IdRemoteStdWinner
        //{
        //    get => idRemouteStdWinner;
        //    set
        //    {
        //        if (idRemouteStdWinner == value) return;
        //        idRemouteStdWinner = value;
        //        OnPropertyChanged();
        //    }
        //}

        private StudentModel stdWinner;
        public StudentModel StdWinner
        {
            get => stdWinner;
            set
            {
                if (stdWinner == value) return;
                stdWinner = value;
            }
        }
    }
}
