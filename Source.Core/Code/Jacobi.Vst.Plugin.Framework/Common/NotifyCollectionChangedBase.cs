using System;
using System.Collections;
using System.Collections.Specialized;

namespace Jacobi.Vst.Framework.Common
{
    /// <summary>
    /// A base class that helps a collection implement the <see cref="INotifyCollectionChanged"/> interface.
    /// </summary>
    public abstract class NotifyCollectionChangedBase : INotifyCollectionChanged
    {
        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Fires the <see cref="CollectionChanged"/> event using single instances.
        /// </summary>
        /// <param name="action">The type of action that caused the collection to change.</param>
        /// <param name="newItem">Can be null.</param>
        /// <param name="oldItem">Can be null.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            var handler = CollectionChanged;

            if (handler != null)
            {
                var args = action switch
                {
                    NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(action, newItem),
                    NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(action, oldItem),
                    NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(action, newItem, oldItem),
                    NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(action),
                    _ => throw new NotSupportedException()
                };
                handler(this, args);
            }
        }

        /// <summary>
        /// Fires the <see cref="CollectionChanged"/> event using lists of items.
        /// </summary>
        /// <param name="action">The type of action that caused the collection to change.</param>
        /// <param name="newItems">Can be null.</param>
        /// <param name="oldItems">Can be null.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            var handler = CollectionChanged;

            if (handler != null)
            {
                var args = action switch
                {
                    NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(action, newItems),
                    NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(action, oldItems),
                    NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(action, newItems, oldItems),
                    NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(action),
                    _ => throw new NotSupportedException()
                };
                handler(this, args);
            }
        }
    }
}
