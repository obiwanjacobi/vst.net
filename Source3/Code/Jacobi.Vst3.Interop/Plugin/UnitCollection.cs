using Jacobi.Vst3.Common;

namespace Jacobi.Vst3.Plugin
{
    public class UnitCollection : KeyedCollectionWithIndex<int, Unit>
    {
        protected override int GetKeyForItem(Unit item)
        {
            if (item == null) return 0;

            return item.Info.Id;
        }
    }
}
