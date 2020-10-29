using System.Windows.Input;

namespace Guess.Yourself
{
    public class WinnersViewModel : NotifyPropertyChanged
    {
        public WinnersView Winners { get; set; }
        public MainWindowViewModel Main { get; set; }
        public WinnersViewModel(MainWindowViewModel main, WinnersView win)
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
                return Main.Winners.Count != 0;
            }
            ));
    }
}
