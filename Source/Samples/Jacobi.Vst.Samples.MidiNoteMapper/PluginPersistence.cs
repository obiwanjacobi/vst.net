namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using System;
    using System.IO;
    using System.Text;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    class PluginPersistence : IVstPluginPersistence
    {
        private Plugin _plugin;
        private Encoding _encoding = Encoding.ASCII;

        public PluginPersistence(Plugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstPluginPersistence Members

        public bool CanLoadChunk(VstPatchChunkInfo chunkInfo)
        {
            return true;
        }

        public void ReadPrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryReader reader = new BinaryReader(stream, _encoding);

            _plugin.NoteMap.Clear();
            int count = reader.ReadInt32();

            for (int n = 0; n < count; n++)
            {
                MapNoteItem item = new MapNoteItem();
                item.KeyName = reader.ReadString();
                item.TriggerNoteNumber = reader.ReadByte();
                item.OutputNoteNumber = reader.ReadByte();

                _plugin.NoteMap.Add(item);
            }
        }

        public void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryWriter writer = new BinaryWriter(stream, _encoding);

            writer.Write(_plugin.NoteMap.Count);

            foreach (MapNoteItem item in _plugin.NoteMap)
            {
                writer.Write(item.KeyName);
                writer.Write(item.TriggerNoteNumber);
                writer.Write(item.OutputNoteNumber);
            }
        }

        #endregion
    }
}
