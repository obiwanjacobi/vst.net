using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Jacobi.Vst.Framework.Common
{
    /// <summary>
    /// A collection class that implements the <see cref="INotifyCollectionChanged"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of collection items.</typeparam>
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged
    {
        /// <inheritdoc/>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, null);
        }

        /// <inheritdoc/>
        protected override void RemoveItem(int index)
        {
            T oldItem = this[index];

            base.RemoveItem(index);

            OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, oldItem);
        }

        /// <inheritdoc/>
        protected override void SetItem(int index, T item)
        {
            T oldItem = this[index];

            base.SetItem(index, item);

            OnCollectionChanged(NotifyCollectionChangedAction.Replace, item, oldItem);
        }

        /// <inheritdoc/>
        protected override void ClearItems()
        {
            ArrayList oldItems = new ArrayList(this);

            base.ClearItems();

            OnCollectionChanged(NotifyCollectionChangedAction.Reset, null, oldItems);
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
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
        }

        /// <summary>
        /// Fires the <see cref="CollectionChanged"/> event using lists of items.
        /// </summary>
        /// <param name="action">The type of action that caused the collection to change.</param>
        /// <param name="newItems">Can be null.</param>
        /// <param name="oldItems">Can be null.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItems, oldItems));
        }

        #endregion
    }
}
