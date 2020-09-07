using System.Windows.Input;

namespace GuessYouSelf.Core
{
    public class WinnersViewModel : NotifyPropertyChanged
    {
        public WinnersView Winners { get; set; }
        public MainWindowViewModelCore Main { get; set; }
        public WinnersViewModel(MainWindowViewModelCore main, WinnersView win)
        {
            Winners = win;
            Winners.DataContext = this;
            this.Main = main;
            Winners.Owner = App.Current.MainWindow;
        }

        public RelayCommand<StudentWinner> winnerClose = null;
        public ICommand WinnerClose => winnerClose ?? (winnerClose = new RelayCommand<StudentWinner>((param) =>
        {
            Winners.Close();
        },
            (param) =>
            {
                return true;
            }
            ));
    }
}
