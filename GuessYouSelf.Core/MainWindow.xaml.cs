namespace GuessYouSelf.Core
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModelCore(new DefaultDialogService(), new TextFileService());
        }
    }
}
