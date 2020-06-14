using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework.Plugin.IO;
using System.IO;
using System.Text;

namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    /// <summary>
    /// The VstPluginPersistenceBase class implements the <see cref="IVstPluginPersistence"/>\
    /// interface for custom persistence.
    /// </summary>
    /// <remarks>
    /// This class must be derived and the abstract <see cref="CreateProgramReader"/> method
    /// must be implemented.
    /// </remarks>
    public abstract class VstPluginPersistence : IVstPluginPersistence
    {
        /// <summary>
        /// Initializes a new instance, assuming ASCII encoding for text.
        /// </summary>
        protected VstPluginPersistence()
        {
            Encoding = Encoding.ASCII;
        }

        /// <summary>
        /// Gets or sets the encoding that is used for text.
        /// </summary>
        public Encoding Encoding { get; set; }

        #region IVstPluginPersistence Members

        /// <inheritdoc />
        public virtual void ReadPrograms(Stream stream, VstProgramCollection programs)
        {
            VstProgramReader reader = CreateProgramReader(stream);

            reader.ReadPrograms(programs);
        }

        /// <inheritdoc />
        public virtual void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            VstProgramWriter writer = new VstProgramWriter(stream, Encoding);

            writer.Write(programs);
        }

        /// <inheritdoc />
        public virtual bool CanLoadChunk(VstPatchChunkInfo chunkInfo)
        {
            return true;
        }

        #endregion

        /// <summary>
        /// Creates a reader for reading the <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">Contains the stream that was written by plugin. Must not be null.</param>
        /// <returns>Returns a new reader. Never returns null.</returns>
        protected abstract VstProgramReader CreateProgramReader(Stream input);
    }
}
