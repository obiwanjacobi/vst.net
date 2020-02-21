using System;
using System.Collections;

namespace Jacobi.Vst.Framework.Common
{
    /// <summary>
    /// Notifies of a change in the collection.
    /// </summary>
    /// <remarks>The interface is modelled after the interface with the same name in .NET 3.0.</remarks>
    public interface INotifyCollectionChanged
    {
        /// <summary>
        /// Fires when the collection content changes.
        /// </summary>
        event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;
    }

    /// <summary>
    /// The event arguments that indicate what items have changed in a collection.
    /// </summary>
    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructs an instance with lists of old and new items.
        /// </summary>
        /// <param name="action">The type of action that caused the change.</param>
        /// <param name="newItems">Can be null.</param>
        /// <param name="oldItems">Can be null.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            Action = action;

            if(newItems != null)
                NewItems = new ArrayList(newItems);
            else
                NewItems = new ArrayList();

            if (oldItems != null)
                OldItems = new ArrayList(oldItems);
            else
                OldItems = new ArrayList();
        }

        /// <summary>
        /// Constructs an instance with a single old and new item instance.
        /// </summary>
        /// <param name="action">The type of action that caused the change.</param>
        /// <param name="newItem">Can be null.</param>
        /// <param name="oldItem">Can be null.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            Action = action;

            NewItems = new ArrayList();
            if(newItem != null)
                NewItems.Add(newItem);

            OldItems = new ArrayList();
            if (oldItem != null)
                OldItems.Add(oldItem);
        }

        /// <summary>
        /// Gets the action that caused the collection to change.
        /// </summary>
        public NotifyCollectionChangedAction Action { get; private set; }

        /// <summary>
        /// Gets the list with items that are new to the collection.
        /// </summary>
        /// <remarks>Will never be null.</remarks>
        public IList NewItems { get; private set; }

        /// <summary>
        /// Gets the list with items that are old to the collection.
        /// </summary>
        /// <remarks>Will never be null.</remarks>
        public IList OldItems { get; private set; }
    }

    /// <summary>
    /// Lists the type of action that cause a collection to change.
    /// </summary>
    public enum NotifyCollectionChangedAction
    {
        /// <summary>New items were added to the collection.</summary>
        Add,
        //Move,
        /// <summary>Items were removed from the collection.</summary>
        Remove,
        /// <summary>Items were replaced in the collection.</summary>
        Replace,
        /// <summary>A drastic change was made to the collection (Clear() for instance).</summary>
        Reset,
    }
}
