namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Common;
    using System.Collections.Generic;

    /// <summary>
    /// Manages a collection of <see cref="VstParameterInfo"/> instances.
    /// </summary>
    public class VstParameterInfoCollection : ObservableCollection<VstParameterInfo>
    {
        /// <summary>
        /// Adds a range of <see cref="VstParameterInfo"/> instances to the collection.
        /// </summary>
        /// <param name="enumerator">Must not be null.</param>
        public void AddRange(IEnumerable<VstParameterInfo> enumerator)
        {
            Throw.IfArgumentIsNull(enumerator, nameof(enumerator));

            foreach (VstParameterInfo paramInfo in enumerator)
            {
                Add(paramInfo);
            }
        }
    }
}
