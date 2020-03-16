using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using RLib;

namespace Guess.Yourself
{
    public partial class MainWindow : Window
    {
        ObservableCollection<StudentModel> students = new ObservableCollection<StudentModel>();


        DeviceManager init = new DeviceManager(new VotumDevicesManager());

        public void FillDataGrid()
        {
            for (int i = 0; i < 32; i++)
            {
                students.Add(new StudentModel());
            }
            TableName.ItemsSource = students;
        }

        public void MyClick()
        {
            init.votumManager.ButtonClicked += DeviceVotum_ButtonClicked;
        }
        private void DeviceVotum_ButtonClicked(object sender, ButtonClickEventArgs e)
        {
            EnsureRemoteAdded(e.ReceiverId, e.RemoteId);
            var newStudent = students.FirstOrDefault(x => x.ReceiverId.Equals(e.ReceiverId) && x.RemoteId.Equals(e.RemoteId));
            if (newStudent == null) throw new Exception("Что-то пошло не так!");
        }

        public void EnsureRemoteAdded(int receiverId, int remoteId)
        {
            if (!students.Any(x => x.ReceiverId.Equals(receiverId) && x.RemoteId.Equals(remoteId)))
            {
                var newTheObjectOfTheStudent = new StudentModel(receiverId, remoteId);
                this.Dispatcher.BeginInvoke(new Action()=> { }
                students.Add(newTheObjectOfTheStudent);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
            MyClick();
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
