using System;
using System.IO;
using System.Text;

using Jacobi.Vst.Core;
using Jacobi.Vst.Framework.Plugin.IO;

namespace Jacobi.Vst.Framework.Plugin
{
    public abstract class VstPluginPersistenceBase : IVstPluginPersistence
    {
        public VstPluginPersistenceBase()
        {
            Encoding = Encoding.ASCII;
        }

        public Encoding Encoding { get; set; }

        #region IVstPluginPersistence Members

        public virtual void ReadPrograms(Stream stream, VstProgramCollection programs)
        {
            VstProgramReaderBase reader = CreateProgramReader(stream);

            reader.ReadPrograms(programs);
        }

        public virtual void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            VstProgramWriter writer = new VstProgramWriter(stream, Encoding);

            writer.Write(programs);
        }

        public virtual bool CanLoadChunk(VstPatchChunkInfo chunkInfo)
        {
            return true;
        }

        #endregion

        protected abstract VstProgramReaderBase CreateProgramReader(Stream input);
    }
}
