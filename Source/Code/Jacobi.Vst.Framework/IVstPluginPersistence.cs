namespace Jacobi.Vst.Framework
{
    using System.IO;
    using Jacobi.Vst.Core;

    public interface IVstPluginPersistence
    {
        /// <summary>
        /// Reads chunks for all programs from the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">Must not be null.</param>
        /// <param name="programs">Receives the new <see cref="VstProgram"/> instances. Must not be null.</param>
        void ReadPrograms(Stream stream, VstProgramCollection programs);
        /// <summary>
        /// Writes chunks for all programs in <paramref name="programs"/> to the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">Must not be null.</param>
        /// <param name="programs">Contains the <see cref="VstProgram"/>s that should be serialized into the <paramref name="stream"/>. Must not be null.</param>
        void WritePrograms(Stream stream, VstProgramCollection programs);

        /// <summary>
        /// Called to verify if specific data version is supported.
        /// </summary>
        /// <param name="chunkInfo">Version info for chunk.</param>
        /// <returns>Returns true if data version is supported, otherwise false is returned.</returns>
        bool CanLoadChunk(VstPatchChunkInfo chunkInfo);
    }
}
