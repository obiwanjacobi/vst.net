namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    public class VstMidiProgramCollection : KeyedCollection<string, VstMidiProgram>
    {
        protected override string GetKeyForItem(VstMidiProgram item)
        {
            return item.Name;
        }
    }
}
