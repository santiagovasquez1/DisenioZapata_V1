using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DisenioZapata_V1.Model
{
    public class NotificationObject : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}