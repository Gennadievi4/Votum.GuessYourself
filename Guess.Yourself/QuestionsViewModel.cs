using System.ComponentModel;

namespace Guess.Yourself
{
    public class QuestionsViewModel
    {
        MainWindowViewModel _MainWindowViewModel;
        public QuestionsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _MainWindowViewModel = mainWindowViewModel;
        }

    }
}
