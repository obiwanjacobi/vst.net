namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using System.Collections.ObjectModel;

    class MapNoteItem
    {
        public string KeyName { get; set; }
        public byte TriggerNoteNumber { get; set; }
        public byte OutputNoteNumber { get; set; }
    }

    class MapNoteItemList : KeyedCollection<byte, MapNoteItem>
    {
        protected override byte GetKeyForItem(MapNoteItem item)
        {
            return item.TriggerNoteNumber;
        }
    }
}
