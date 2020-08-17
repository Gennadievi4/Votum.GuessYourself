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
    }
}
