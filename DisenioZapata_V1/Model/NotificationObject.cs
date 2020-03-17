using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class NotificationObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}