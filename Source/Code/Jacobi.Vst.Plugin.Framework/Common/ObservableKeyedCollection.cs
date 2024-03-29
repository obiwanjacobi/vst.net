﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Jacobi.Vst.Plugin.Framework.Common
{
    /// <summary>
    /// A KeyedCollection base class that implements the <see cref="INotifyCollectionChanged"/> interface.
    /// </summary>
    /// <typeparam name="KeyT">The type of collection item keys.</typeparam>
    /// <typeparam name="ValueT">The type of collection items.</typeparam>
    public abstract class ObservableKeyedCollection<KeyT, ValueT> : KeyedCollection<KeyT, ValueT>, INotifyCollectionChanged
        where KeyT : notnull
    {
        /// <summary>
        /// Contructs an empty instance.
        /// </summary>
        protected ObservableKeyedCollection()
        { }

        /// <summary>
        /// Constructs a prefilled instance.
        /// </summary>
        /// <param name="items">Must not be null.</param>
        protected ObservableKeyedCollection(IEnumerable<ValueT> items)
        {
            int i = 0;
            foreach (var item in items)
            {
                SetItem(i, item);
                i++;
            }
        }

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
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Fires the <see cref="CollectionChanged"/> event using single instances.
        /// </summary>
        /// <param name="action">The type of action that caused the collection to change.</param>
        /// <param name="newItem">Can be null.</param>
        /// <param name="oldItem">Can be null.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action, object? newItem, object? oldItem)
        {
            var args = action switch
            {
                NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(action, newItem),
                NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(action, oldItem),
                NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(action, newItem, oldItem),
                NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(action),
                _ => throw new NotSupportedException()
            };
            CollectionChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Constructs an instance with lists of old and new items.
        /// </summary>
        /// <param name="action">The type of action that caused the change.</param>
        /// <param name="newItems">Can be null.</param>
        /// <param name="oldItems">Can be null.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action, IList? newItems, IList? oldItems)
        {
            var args = action switch
            {
                NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(action, newItems),
                NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(action, oldItems),
                NotifyCollectionChangedAction.Replace => newItems is not null && oldItems is not null 
                    ? new NotifyCollectionChangedEventArgs(action, newItems, oldItems)
                    : throw new ArgumentNullException($"For a Replace action both {nameof(newItems)} and {nameof(oldItems)} must be non-null."),
                NotifyCollectionChangedAction.Reset => new NotifyCollectionChangedEventArgs(action),
                _ => throw new NotSupportedException($"Notify Collection Changed Action {action.ToString()} is not supported.")
            };
            CollectionChanged?.Invoke(this, args);
        }

        #endregion
    }
}
