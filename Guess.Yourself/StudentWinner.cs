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

        private string textWinnersTable;
        public string TextWinnersTable
        {
            get => textWinnersTable;
            set
            {
                if (textWinnersTable == value) return;
                textWinnersTable = value;
                OnPropertyChanged();
            }
        }

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
