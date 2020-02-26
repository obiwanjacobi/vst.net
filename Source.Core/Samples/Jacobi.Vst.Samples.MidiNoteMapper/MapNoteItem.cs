namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents one note mapping.
    /// </summary>
    class MapNoteItem
    {
        /// <summary>
        /// Gets or sets a readable name for this mapping item.
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the note number that triggers this mapping item.
        /// </summary>
        public byte TriggerNoteNumber { get; set; }

        /// <summary>
        /// Gets or sets the note number that is sent to the output when this item is triggered.
        /// </summary>
        public byte OutputNoteNumber { get; set; }
    }

    /// <summary>
    /// Manages a list of <see cref="MapNoteItem"/> instances.
    /// </summary>
    class MapNoteItemList : KeyedCollection<byte, MapNoteItem>
    {
        /// <summary>
        /// Returns <see cref="MapNoteItem.TriggerNoteNumber"/>.
        /// </summary>
        /// <param name="item">Must not be null.</param>
        /// <returns>Returns the key for the <paramref name="item"/>.</returns>
        protected override byte GetKeyForItem(MapNoteItem item)
        {
            return item.TriggerNoteNumber;
        }
    }
}
