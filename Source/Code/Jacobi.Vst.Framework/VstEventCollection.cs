namespace Jacobi.Vst.Framework
{
    using System.Collections.Generic;
    using Jacobi.Vst.Core;
    using System;

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

        #region IList<VstEvent> Members

        public int IndexOf(VstEvent item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, VstEvent item)
        {
            ThrowIfReadOnly();

            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ThrowIfReadOnly();

            _list.RemoveAt(index);
        }

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

        public void Add(VstEvent item)
        {
            ThrowIfReadOnly();

            _list.Add(item);
        }

        public void Clear()
        {
            ThrowIfReadOnly();

            _list.Clear();
        }

        public bool Contains(VstEvent item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(VstEvent[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public bool Remove(VstEvent item)
        {
            ThrowIfReadOnly();

            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<VstEvent> Members

        public IEnumerator<VstEvent> GetEnumerator()
        {
            return _list.GetEnumerator(); ;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("The VstEventCollection is read-only.");
            }
        }
    }
}
