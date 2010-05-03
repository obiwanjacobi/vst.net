namespace Jacobi.Vst.Framework
{
    using System.Collections.Generic;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

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
            Throw.IfArgumentIsNull(enumerator, "enumerator");

            foreach (VstParameterInfo paramInfo in enumerator)
            {
                Add(paramInfo);
            }
        }
    }
}
