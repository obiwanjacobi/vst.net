using Jacobi.Vst3.Common;

namespace Jacobi.Vst3.Plugin
{
    public class UnitCollection : KeyedCollectionWithIndex<int, Unit>
    {
        public Unit Parent { get; set; }

        protected override int GetKeyForItem(Unit item)
        {
            if (item == null) return 0;

            return item.Info.Id;
        }

        protected override void InsertItem(int index, Unit item)
        {
            EnsureParent(item);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, Unit item)
        {
            EnsureParent(item);
            base.SetItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            // detach from parent
            var unit = this.GetAt(index);
            unit.Parent = null;

            base.RemoveItem(index);
        }

        private void EnsureParent(Unit item)
        {
            if (Parent != null && 
                item != null && 
                item.Parent == null)
            {
                item.Parent = Parent;
            }
        }
    }
}
