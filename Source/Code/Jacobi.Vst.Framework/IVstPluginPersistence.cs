namespace Jacobi.Vst.Framework
{
    using System.IO;

    public interface IVstPluginPersistence
    {
        /// <summary>
        /// Reads chunks for a single, active program from the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">Must not be null.</param>
        /// <returns>Never returns null.</returns>
        VstProgram ReadProgram(Stream stream);
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
        /// <param name="programs">Must not be null.</param>
        void WritePrograms(Stream stream, VstProgramCollection programs);
    }
}
