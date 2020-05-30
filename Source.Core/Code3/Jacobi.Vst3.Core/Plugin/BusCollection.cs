using Jacobi.Vst3.Core;
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
    }
}
