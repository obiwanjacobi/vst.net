using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework.Common;
using System.Collections.Generic;

namespace Jacobi.Vst.Plugin.Framework
{
    /// <summary>
    /// Manages a collection of <see cref="VstMidiCategory"/> instances.
    /// </summary>
    public class VstMidiCategoryCollection : ObservableKeyedCollection<string, VstMidiCategory>
    {
        /// <summary>
        /// Contructs an empty instance.
        /// </summary>
        public VstMidiCategoryCollection()
        { }

        /// <summary>
        /// Constructs a prefilled instance.
        /// </summary>
        /// <param name="midiCategories">Must not be null.</param>
        public VstMidiCategoryCollection(IEnumerable<VstMidiCategory> midiCategories)
            : base(midiCategories)
        { }

        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstMidiCategory.Name"/> property.</returns>
        protected override string GetKeyForItem(VstMidiCategory item)
        {
            Throw.IfArgumentIsNull(item, nameof(item));

            return item.Name;
        }
    }
}
