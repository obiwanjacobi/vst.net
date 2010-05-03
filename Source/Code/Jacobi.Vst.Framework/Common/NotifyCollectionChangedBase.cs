using System;
using System.Collections;

namespace Jacobi.Vst.Framework.Common
{
    public abstract class NotifyCollectionChangedBase : INotifyCollectionChanged
    {
        public event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;

        protected void OnCollectionChanged(NotifyColletionChangedAction action, object newItem, object oldItem)
        {
            var handler = CollectionChanged;

            if (handler != null)
            {
                handler(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
            }
        }

        protected void OnCollectionChanged(NotifyColletionChangedAction action, IList newItems, IList oldItems)
        {
            var handler = CollectionChanged;

            if (handler != null)
            {
                handler(this, new NotifyCollectionChangedEventArgs(action, newItems, oldItems));
            }
        }
    }
}
