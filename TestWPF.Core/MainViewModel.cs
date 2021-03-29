using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestWPF.Core
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<MyItem> _myItems;
        private ICommand _myCommand;
        public ICommand MyCommand => _myCommand ??= new RelayCommand(parameter =>
        {
            if (parameter is MyItem item)
            {
                MessageBox.Show(item.MyValue);
                Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    item.Enabled = !item.Enabled;
                    // так как вызов CommandManager из стороннего потока не сработает, вызываю через диспетчер
                    Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
                });
            }
        }, parameter => parameter is MyItem item && item.Enabled && item.MyValue?.Length > 0);

        public ObservableCollection<MyItem> MyItems
        {
            get => _myItems;
            set
            {
                _myItems = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            MyItems = new ObservableCollection<MyItem>
        {
            new MyItem { MyValue = "Первая строка" },
            new MyItem { MyValue = "Вторая строка" },
            new MyItem { MyValue = "" }
        };
        }
    }
}
