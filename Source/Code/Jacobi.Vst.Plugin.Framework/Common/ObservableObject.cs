using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Jacobi.Vst.Plugin.Framework.Common
{
    /// <summary>
    /// A base class for class that implement the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property on the class (this) that has changed.</param>
        /// <remarks>DEBUG builds veriry that the <paramref name="propertyName"/> 
        /// is actually found on the instance using reflection.</remarks>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyExists(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Helper method for derived classes when setting a value on a property.
        /// </summary>
        /// <typeparam name="T">The data type of the property value.</typeparam>
        /// <param name="value">The value of the property to set.</param>
        /// <param name="field">A reference to the storage or backing field of the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>Returns true when the value was actually set. 
        /// When the property already has the specified <paramref name="value"/> false is returned 
        /// and no event is raied.</returns>
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
            var _ = GetType().GetProperty(propertyName)
                ?? throw new InvalidOperationException("OnPropertyChanged: Property does not exist: " + propertyName);
        }
    }
}
