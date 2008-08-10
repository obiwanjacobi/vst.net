namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstMidiProgram"/> instances.
    /// </summary>
    public class VstMidiProgramCollection : KeyedCollection<string, VstMidiProgram>
    {
        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstMidiProgram.Name"/> property.</returns>
        protected override string GetKeyForItem(VstMidiProgram item)
        {
            return item.Name;
        }
    }
}
