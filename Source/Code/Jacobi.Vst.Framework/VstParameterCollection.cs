namespace Jacobi.Vst.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// Manages a collection of <see cref="VstParameter"/> instances.
    /// </summary>
    public class VstParameterCollection : ObservableKeyedCollection<string, VstParameter>, IActivatable
    {
        /// <summary>
        /// Adds a range of <paramref name="paremeters"/> to the collection.
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
        public void AddRange(IEnumerable<VstParameter> parameters)
        {
            Throw.IfArgumentIsNull(parameters, "parameters");

            foreach (VstParameter param in parameters)
            {
                Add(param);
            }
        }

        /// <summary>
        /// Returns a collection of <see cref="VstParameter"/> instances that all belong
        /// to the specified <paramref name="category"/>.
        /// </summary>
        /// <param name="category">The parameter category. Can be null.</param>
        /// <returns>An empty collection is returned when no parameters could be found that
        /// fall under the specified <paramref name="category"/>. Never returns null.</returns>
        public VstParameterCollection FindParametersIn(VstParameterCategory category)
        {
            VstParameterCollection results = new VstParameterCollection();

            foreach (VstParameter param in this.Items)
            {
                if (param.Info.Category == category)
                {
                    results.Add(param);
                }
            }

            return results;
        }

        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstParameterInfo.Name"/> property.</returns>
        protected override string GetKeyForItem(VstParameter item)
        {
            return item.Info.Name;
        }

        /// <summary>
        /// Override to <see cref="VstParameter.Dispose()"/> the collection items.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (VstParameter parameter in this)
            {
                parameter.Dispose();
            }

            base.ClearItems();
        }

        /// <summary>
        /// Override to <see cref="VstParameter.Dispose()"/> the collection item.
        /// </summary>
        /// <param name="index">A zero-based index that will be removed.</param>
        protected override void RemoveItem(int index)
        {
            VstParameter parameter = this[index];

            base.RemoveItem(index);

            parameter.Dispose();
        }

        /// <summary>
        /// Override to <see cref="VstParameter.Dispose()"/> the collection item.
        /// </summary>
        /// <param name="index">A zero-based index the new <paramref name="item"/> will be set.</param>
        /// <param name="item">The new item to set. Can be null.</param>
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

        private bool _isActive;
        /// <summary>
        /// Gets the parameter collection status.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;

                foreach (VstParameter param in this.Items)
                {
                    param.IsActive = value;
                }
            }
        }

        #endregion
    }
}
