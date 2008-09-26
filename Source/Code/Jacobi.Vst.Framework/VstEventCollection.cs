namespace Jacobi.Vst.Framework
{
    using System.Collections.Generic;
    using Jacobi.Vst.Core;
    using System;

    /// <summary>
    /// Manages a collection of <see cref="VstEvent"/> instances.
    /// </summary>
    /// <remarks>The collection can be read-only or writable.</remarks>
    public class VstEventCollection : IList<VstEvent>
    {
        private List<VstEvent> _list;

        /// <summary>
        /// Constructs a read/write collection
        /// </summary>
        public VstEventCollection()
        {
            _list = new List<VstEvent>();
        }

        /// <summary>
        /// Constructs a readonly collection.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        public VstEventCollection(VstEvent[] events)
        {
            _list = new List<VstEvent>(events);
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
            Throw.IfArgumentIsNull(events, "events");

            foreach (VstEvent evnt in events)
            {
                Add(evnt);
            }
        }

        /// <summary>
        /// Returns the collection of <see cref="VstEvent"/> instances as an array.
        /// </summary>
        /// <returns>Returns a copy of the <see cref="VstEvent"/> instances.</returns>
        public VstEvent[] ToArray()
        {
            return _list.ToArray();
        }

        #region IList<VstEvent> Members
        
        /// <summary>
        /// Returns the index of the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item to retrieve the index for.</param>
        /// <returns>Returns -1 when the <paramref name="item"/> was not found.</returns>
        public int IndexOf(VstEvent item)
        {
            return _list.IndexOf(item);
        }

        /// <summary>
        /// Inserts a new <paramref name="item"/> at the specified <paramref name="index"/> into the collection.
        /// </summary>
        /// <param name="index">The index to insert the <paramref name="item"/> at.</param>
        /// <param name="item">The <see cref="VstEvent"/> instance to insert.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only.</exception>
        public void Insert(int index, VstEvent item)
        {
            ThrowIfReadOnly();

            _list.Insert(index, item);
        }

        /// <summary>
        /// Removes an item from the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index of the item to remove.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only.</exception>
        public void RemoveAt(int index)
        {
            ThrowIfReadOnly();

            _list.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets an instance at a specific <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item.</param>
        /// <returns>Returns the item at <paramref name="index"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only and the setter is called.</exception>
        public VstEvent this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                ThrowIfReadOnly();

                _list[index] = value;
            }
        }

        #endregion

        #region ICollection<VstEvent> Members

        /// <summary>
        /// Adds a new instance to the collection.
        /// </summary>
        /// <param name="item">A new item to add.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only.</exception>
        public void Add(VstEvent item)
        {
            ThrowIfReadOnly();

            _list.Add(item);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only.</exception>
        public void Clear()
        {
            ThrowIfReadOnly();

            _list.Clear();
        }

        /// <summary>
        /// Checks if the <paramref name="item"/> is part of the collection.
        /// </summary>
        /// <param name="item">The instance to check.</param>
        /// <returns>Returns true when the <paramref name="item"/> is part of the collection.</returns>
        public bool Contains(VstEvent item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Copies the contents of the collection to the specified <paramref name="array"/> starting at the <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">Must not be null.</param>
        /// <param name="arrayIndex">The zero-based index into <paramref name="array"/> to start filling items.</param>
        public void CopyTo(VstEvent[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        private bool _isReadOnly;
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
        /// Removes the specified <paramref name="item"/> from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Returns true when the <paramref name="item"/> was successful removed. 
        /// False is returned when the <paramref name="item"/> was not found in the collection.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the collection is read-only.</exception>
        public bool Remove(VstEvent item)
        {
            ThrowIfReadOnly();

            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<VstEvent> Members

        /// <summary>
        /// Returns an enumeration object.
        /// </summary>
        /// <returns>Never returns null.</returns>
        public IEnumerator<VstEvent> GetEnumerator()
        {
            return _list.GetEnumerator(); ;
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Retrieves a new enumeration object.
        /// </summary>
        /// <returns>Never returns null.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

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
