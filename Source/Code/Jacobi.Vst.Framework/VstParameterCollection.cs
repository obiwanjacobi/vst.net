namespace Jacobi.Vst.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Jacobi.Vst.Core;

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
        /// Override to <see cref="VstParameter.Dispose"/> the collection items.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (VstParameter parameter in this)
            {
                parameter.Dispose();
            }

            base.ClearItems();
            OnChanged();
        }

        /// <summary>
        /// Override to trigger the <see cref="Changed"/> event.
        /// </summary>
        /// <param name="index">A zero-based index the new <paramref name="item"/> will be inserted at.</param>
        /// <param name="item">The item to insert. Can be null.</param>
        protected override void InsertItem(int index, VstParameter item)
        {
            base.InsertItem(index, item);

            OnChanged();
        }

        /// <summary>
        /// Override to <see cref="VstParameter.Dispose"/> the collection item.
        /// </summary>
        /// <param name="index">A zero-based index that will be removed.</param>
        protected override void RemoveItem(int index)
        {
            VstParameter parameter = this[index];

            base.RemoveItem(index);

            parameter.Dispose();

            OnChanged();
        }

        /// <summary>
        /// Override to <see cref="VstParameter.Dispose"/> the collection item.
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

            OnChanged();
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

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Fires after a <see cref="VstParameter"/> has been added or removed from the collection.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        /// <summary>
        /// Raises the <see cref="Changed"/> event.
        /// </summary>
        protected virtual void OnChanged()
        {
            EventHandler<EventArgs> temp = Changed;

            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
