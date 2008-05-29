namespace Jacobi.Vst.Framework
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class VstParameterCollection : KeyedCollection<string, VstParameter>, IActivatable
    {
        public void AddRange(IEnumerable<VstParameter> parameters)
        {
            foreach (VstParameter param in parameters)
            {
                Add(param);
            }
        }

        public VstParameterCollection FindParametersIn(VstParameterCategory category)
        {
            VstParameterCollection results = new VstParameterCollection();

            foreach (VstParameter param in this.Items)
            {
                if (param.Category == category)
                {
                    results.Add(param);
                }
            }

            return results;
        }

        protected override string GetKeyForItem(VstParameter item)
        {
            return item.Info.Name;
        }

        protected override void ClearItems()
        {
            foreach (VstParameter parameter in this)
            {
                parameter.Dispose();
            }

            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            VstParameter parameter = this[index];

            base.RemoveItem(index);

            parameter.Dispose();
        }

        protected override void SetItem(int index, VstParameter item)
        {
            VstParameter parameter = this[index];

            if (parameter != item)
            {
                parameter.Dispose();
            }

            base.SetItem(index, item);
        }

        #region IActivatable Members

        public bool IsActive { get; private set; }

        public void Activate()
        {
            foreach (VstParameter param in this.Items)
            {
                param.Activate();
            }

            IsActive = true;
        }

        public void Deactivate()
        {
            foreach (VstParameter param in this.Items)
            {
                param.Deactivate();
            }

            IsActive = false;
        }

        #endregion
    }
}
