namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    public class VstParameterCategoryCollection : KeyedCollection<string, VstParameterCategory>
    {
        protected override string GetKeyForItem(VstParameterCategory item)
        {
            return item.Name;
        }
    }
}
