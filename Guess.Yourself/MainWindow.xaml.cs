using System.Collections.ObjectModel;
using System.Windows;

namespace Guess.Yourself
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TheObjectOfTheStudent> students = new ObservableCollection<TheObjectOfTheStudent>();

        public void FillDataGrid()
        {
            for (int i = 0; i < 32; i++)
            {
                students.Add(new TheObjectOfTheStudent());
            }
            TableName.ItemsSource = students;
        }
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void YesClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDontKnow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
