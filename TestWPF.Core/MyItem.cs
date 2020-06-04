namespace TestWPF.Core
{
    public class MyItem : NotifyPropertyChanged
    {
        private string _myValue;
        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
        public string MyValue
        {
            get => _myValue;
            set
            {
                _myValue = value;
                OnPropertyChanged();
            }
        }
    }
}
