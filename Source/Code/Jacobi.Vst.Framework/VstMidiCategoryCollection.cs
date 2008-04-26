namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    public class VstMidiCategoryCollection : KeyedCollection<string, VstMidiCategory>
    {
        protected override string GetKeyForItem(VstMidiCategory item)
        {
            return item.Name;
        }
    }
}
