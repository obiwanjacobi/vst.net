namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    /// <summary>
    /// Manages a collection of <see cref="VstEvent"/> instances.
    /// </summary>
    /// <remarks>The collection can be read-only or writable.</remarks>
    public class VstEventCollection : ObservableCollection<VstEvent>
    {
        /// <summary>
        /// Constructs a read/write collection
        /// </summary>
        public VstEventCollection()
        { }

        /// <summary>
        /// Constructs a readonly collection.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        public VstEventCollection(VstEvent[] events)
            : base(events)
        {
            Throw.IfArgumentIsNull(events, nameof(events));

            _isReadOnly = true;
        }

        /// <summary>
        /// Adds a range of <see cref="VstEvent"/> instance.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="events"/> 
        /// is not set to an instance of an object.</exception>
        public void AddRange(IEnumerable<VstEvent> events)
        {
            Throw.IfArgumentIsNull(events, nameof(events));
            var eventList = events.ToList();

            eventList.ForEach(e => Add(e));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, eventList));
        }

        /// <summary>
        /// Overriden to check for IsReadOnly
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            ThrowIfReadOnly();
            base.OnCollectionChanged(e);
        }

        private readonly bool _isReadOnly;
        /// <summary>
        /// Gets a value indicating wheter the collection is read-only.
        /// </summary>
        /// <remarks>All methods that modify the collection will throw an 
        /// <see cref="InvalidOperationException"/> when the collection is read-only.</remarks>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        /// <summary>
        /// Helper method to throw an exception when the collection is read-only.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only.</exception>
        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException(Properties.Resources.VstEventCollection_CollectionReadOnly);
            }
        }
    }
}
