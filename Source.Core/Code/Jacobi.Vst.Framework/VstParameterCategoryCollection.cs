namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// Manages a collection of <see cref="VstParameterCategory"/> instances.
    /// </summary>
    public class VstParameterCategoryCollection : ObservableKeyedCollection<string, VstParameterCategory>
    {
        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstParameterCategory.Name"/> property.</returns>
        protected override string GetKeyForItem(VstParameterCategory item)
        {
            if (item == null) return null;

            return item.Name;
        }

        /// <summary>
        /// Adds all instances in the <paramref name="categories"/> collection to this instance.
        /// </summary>
        /// <param name="categories">Must not be null.</param>
        public void AddRange(VstParameterCategoryCollection categories)
        {
            Throw.IfArgumentIsNull(categories, nameof(categories));

            foreach (VstParameterCategory paramCat in categories)
            {
                Add(paramCat);
            }
        }
    }
}
