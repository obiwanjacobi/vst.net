namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstParameterCategory"/> instances.
    /// </summary>
    public class VstParameterCategoryCollection : KeyedCollection<string, VstParameterCategory>
    {
        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstParameterCategory.Name"/> property.</returns>
        protected override string GetKeyForItem(VstParameterCategory item)
        {
            return item.Name;
        }

        public void AddRange(VstParameterCategoryCollection categories)
        {
            foreach (VstParameterCategory paramCat in categories)
            {
                Add(paramCat);
            }
        }
    }
}
