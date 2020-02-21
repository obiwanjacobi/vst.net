using System;
using System.Collections;

namespace Jacobi.Vst.Framework.Common
{
    /// <summary>
    /// A base class that helps a collection implement the <see cref="INotifyCollectionChanged"/> interface.
    /// </summary>
    public abstract class NotifyCollectionChangedBase : INotifyCollectionChanged
    {
        /// <inheritdoc/>
        public event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;

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
                handler(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
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
                handler(this, new NotifyCollectionChangedEventArgs(action, newItems, oldItems));
            }
        }
    }
}
