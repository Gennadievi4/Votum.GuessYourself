using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Guess.Yourself
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            
            InitializeComponent();
            DataContext = new MainWindowViewModel(new DefaultDialogService(), new TextFileService());
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UIElement senderElement = sender as UIElement;
            UIElement focusedElement = FocusManager.GetFocusedElement(senderElement) as UIElement;
            /*bool result = */focusedElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
            Keyboard.Focus(focusedElement);
            
            //if (result)
            //{
            //    focusedElement.Focus();
            //    Keyboard.Focus(focusedElement);
            //}
            Debug.WriteLine(focusedElement);
        }
    }
}