using System.Windows.Input;

namespace Guess.Yourself
{
    public class WinnersViewModel : NotifyPropertyChanged
    {
        public WinnersView winners { get; set; }
        public MainWindowViewModel Main { get; set; }
        public WinnersViewModel(MainWindowViewModel main, WinnersView win)
        {
            winners = win;
            winners.DataContext = this;
            this.Main = main;
            winners.Owner = App.Current.MainWindow;
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
