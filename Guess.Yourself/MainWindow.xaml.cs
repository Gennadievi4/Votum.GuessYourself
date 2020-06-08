namespace Guess.Yourself
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new DefaultDialogService(), new TextFileService());
        }
    }
}