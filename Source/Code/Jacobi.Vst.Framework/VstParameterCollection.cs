namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    public class VstParameterCollection : KeyedCollection<string, VstParameter>
    {
        protected override string GetKeyForItem(VstParameter item)
        {
            return item.Name;
        }
    }
}
