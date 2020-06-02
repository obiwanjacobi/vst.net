using System;
using System.Collections;
using System.Collections.Specialized;

namespace Jacobi.Vst.Plugin.Framework.Common
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
            CollectionChanged?.Invoke(this, CreateNotifyCollectionChangedEventArgs(action, newItem, oldItem));
        }

        /// <summary>
        /// Fires the <see cref="CollectionChanged"/> event using lists of items.
        /// </summary>
        /// <param name="action">The type of action that caused the collection to change.</param>
        /// <param name="newItems">Can be null.</param>
        /// <param name="oldItems">Can be null.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            CollectionChanged?.Invoke(this, CreateNotifyCollectionChangedEventArgs(action, newItems, oldItems));
        }

        internal static NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            return action switch
            {
                NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(action, newItem),
                NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(action, oldItem),
                NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(action, newItem, oldItem),
                NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(action),
                _ => throw new NotSupportedException()
            };
        }

        internal static NotifyCollectionChangedEventArgs CreateNotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            return action switch
            {
                NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(action, newItems),
                NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(action, oldItems),
                NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(action, newItems, oldItems),
                NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(action),
                _ => throw new NotSupportedException()
            };
        }
    }
}
