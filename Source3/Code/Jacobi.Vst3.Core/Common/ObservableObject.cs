using System.ComponentModel;

namespace Jacobi.Vst3.Common
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
