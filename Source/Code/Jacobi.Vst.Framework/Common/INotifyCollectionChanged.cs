using System;
using System.Collections;

namespace Jacobi.Vst.Framework.Common
{
    public interface INotifyCollectionChanged
    {
        event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;
    }

    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        public NotifyCollectionChangedEventArgs(NotifyColletionChangedAction action, IList newItems, IList oldItems)
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

        public NotifyCollectionChangedEventArgs(NotifyColletionChangedAction action, object newItem, object oldItem)
        {
            Action = action;

            NewItems = new ArrayList();
            if(newItem != null)
                NewItems.Add(newItem);

            OldItems = new ArrayList();
            if (oldItem != null)
                OldItems.Add(oldItem);
        }

        public NotifyColletionChangedAction Action { get; private set; }
        public IList NewItems { get; private set; }
        public IList OldItems { get; private set; }
    }

    public enum NotifyColletionChangedAction
    {
        Add,
        //Move,
        Remove,
        Replace,
        Reset,
    }
}
