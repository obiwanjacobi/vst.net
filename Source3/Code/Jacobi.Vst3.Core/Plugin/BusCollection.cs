using Jacobi.Vst3.Core;
using System;
using System.Collections.ObjectModel;

namespace Jacobi.Vst3.Plugin
{
    // BusList
    public sealed class BusCollection : KeyedCollection<string, Bus>
    {
        public BusCollection(MediaTypes mediaType, BusDirections busDir)
        {
            MediaType = mediaType;
            BusDirection = busDir;
        }

        public MediaTypes MediaType { get; private set; }

        public BusDirections BusDirection { get; private set; }

        protected override string GetKeyForItem(Bus item)
        {
            if (item == null) return null;

            return item.Name;
        }

        protected override void InsertItem(int index, Bus item)
        {
            ThrowIfNotOfMediaType(item);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, Bus item)
        {
            ThrowIfNotOfMediaType(item);
            base.SetItem(index, item);
        }

        private void ThrowIfNotOfMediaType(Bus item)
        {
            if (item != null && item.MediaType != MediaType)
            {
                throw new ArgumentException("The MediaType for the item does not match the collection.", nameof(item));
            }
        }
    }
}
