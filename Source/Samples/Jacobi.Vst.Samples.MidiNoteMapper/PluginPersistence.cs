namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework;
    using System;
    using System.IO;
    using System.Text;

    internal sealed class PluginPersistence : IVstPluginPersistence
    {
        private readonly MapNoteItemList _noteMap;
        private readonly Encoding _encoding = Encoding.ASCII;

        public PluginPersistence(MapNoteItemList noteMap)
        {
            _noteMap = noteMap ?? throw new ArgumentNullException(nameof(noteMap));
        }

        #region IVstPluginPersistence Members

        public bool CanLoadChunk(VstPatchChunkInfo chunkInfo)
        {
            return true;
        }

        public void ReadPrograms(Stream stream, VstProgramCollection programs)
        {
            var reader = new BinaryReader(stream, _encoding);

            _noteMap.Clear();
            int count = reader.ReadInt32();

            for (int n = 0; n < count; n++)
            {
                var item = new MapNoteItem
                {
                    KeyName = reader.ReadString(),
                    TriggerNoteNumber = reader.ReadByte(),
                    OutputNoteNumber = reader.ReadByte()
                };

                _noteMap.Add(item);
            }
        }

        public void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            var writer = new BinaryWriter(stream, _encoding);

            writer.Write(_noteMap.Count);

            foreach (MapNoteItem item in _noteMap)
            {
                writer.Write(item.KeyName);
                writer.Write(item.TriggerNoteNumber);
                writer.Write(item.OutputNoteNumber);
            }
        }

        #endregion
    }
}
