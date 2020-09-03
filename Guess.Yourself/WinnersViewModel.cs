using System.Windows.Input;

namespace Guess.Yourself
{
    public class WinnersViewModel : NotifyPropertyChanged
    {
        public AsyncObservableCollection<StudentWinner> winnersStd { get; set; }
        private WinnersView winners;
        private MainWindowViewModel main;
        public WinnersViewModel(MainWindowViewModel main, WinnersView win)
        {
            winners = win;
            winners.DataContext = this;
            this.main = main;
            winners.Owner = App.Current.MainWindow;
            winnersStd = main.Winners;
        }

        public RelayCommand<StudentWinner> winnerClose = null;
        public ICommand WinnerClose => winnerClose ?? (winnerClose = new RelayCommand<StudentWinner>((param) =>
        {
            winners.Close();
        },
            (param) =>
            {
                return true;
            }
            ));
    }
}
