using System.ComponentModel;

namespace Guess.Yourself
{
    public class QuestionsViewModel : INotifyPropertyChanged
    {
        MainWindowViewModel _MainWindowViewModel;
        public QuestionsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _MainWindowViewModel = mainWindowViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
