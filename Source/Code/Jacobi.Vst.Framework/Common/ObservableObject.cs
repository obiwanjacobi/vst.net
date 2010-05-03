using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Jacobi.Vst.Framework.Common
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyExists(propertyName);

            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetProperty<T>(T value, ref T field, string propertyName)
        {
            if (!Equals(value, field))
            {
                field = value;
                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyExists(string propertyName)
        {
            PropertyInfo propInfo = GetType().GetProperty(propertyName);

            if (propInfo == null)
            {
                throw new InvalidOperationException("OnPropertyChanged: Property does not exist: " + propertyName);
            }
        }
    }
}
