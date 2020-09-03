namespace Guess.Yourself
{
    public partial class MainWindow
    {
        MainWindowViewModel main;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new DefaultDialogService(), new TextFileService());
        }
    }
}