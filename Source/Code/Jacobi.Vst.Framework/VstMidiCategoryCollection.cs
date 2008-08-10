﻿namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstMidiCategory"/> instances.
    /// </summary>
    public class VstMidiCategoryCollection : KeyedCollection<string, VstMidiCategory>
    {
        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstMidiCategory.Name"/> property.</returns>
        protected override string GetKeyForItem(VstMidiCategory item)
        {
            return item.Name;
        }
    }
}
