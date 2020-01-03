using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BindingBugExample.MVVM
{
    public class BaseViewModel : INotifyPropertyChanged
   {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName] string propertyName = null)
        {
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
