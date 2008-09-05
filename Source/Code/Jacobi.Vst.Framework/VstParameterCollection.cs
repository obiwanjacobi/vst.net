namespace Jacobi.Vst.Framework
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstParameter"/> instances.
    /// </summary>
    public class VstParameterCollection : KeyedCollection<string, VstParameter>, IActivatable
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
                if (param.Category == category)
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
        /// Override to <see cref="VstParameter.Dispose"/> the collection items.
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
        /// Override to <see cref="VstParameter.Dispose"/> the collection item.
        /// </summary>
        protected override void RemoveItem(int index)
        {
            VstParameter parameter = this[index];

            base.RemoveItem(index);

            parameter.Dispose();
        }

        /// <summary>
        /// Override to <see cref="VstParameter.Dispose"/> the collection item.
        /// </summary>
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

        /// <summary>
        /// Gets the parameter collection status.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Activates all parameters in the collection.
        /// </summary>
        public void Activate()
        {
            foreach (VstParameter param in this.Items)
            {
                param.Activate();
            }

            IsActive = true;
        }

        /// <summary>
        /// Deactivates all parameters in the collection.
        /// </summary>
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
