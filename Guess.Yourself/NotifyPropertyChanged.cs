using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Guess.Yourself
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Get(propertyName));
    }
}