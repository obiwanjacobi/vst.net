using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Jacobi.Vst.Framework.Common
{
    /// <summary>
    /// A KeyedCollection base class that implements the <see cref="INotifyCollectionChanged"/> interface.
    /// </summary>
    /// <typeparam name="KeyT">The type of collection item keys.</typeparam>
    /// <typeparam name="ValueT">The type of collection items.</typeparam>
    public abstract class ObservableKeyedCollection<KeyT, ValueT> : KeyedCollection<KeyT, ValueT>, INotifyCollectionChanged
    {
        /// <inheritdoc/>
        protected override void ClearItems()
        {
            ArrayList oldItems = new ArrayList(this);

            base.ClearItems();

            OnCollectionChanged(NotifyCollectionChangedAction.Reset, null, oldItems);
        }
        
        /// <inheritdoc/>
        protected override void InsertItem(int index, ValueT item)
        {
            base.InsertItem(index, item);

            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, null);
        }

        /// <inheritdoc/>
        protected override void RemoveItem(int index)
        {
            ValueT oldItem = this[index];

            base.RemoveItem(index);

            OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, oldItem);
        }

        /// <inheritdoc/>
        protected override void SetItem(int index, ValueT item)
        {
            ValueT oldItem = this[index];

            base.SetItem(index, item);

            OnCollectionChanged(NotifyCollectionChangedAction.Replace, item, oldItem);
        }

        #region INotifyCollectionChanged Members

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
        /// Constructs an instance with lists of old and new items.
        /// </summary>
        /// <param name="action">The type of action that caused the change.</param>
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

        #endregion
    }
}
