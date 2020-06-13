namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstParameterInfo"/> instances.
    /// </summary>
    public class VstParameterInfoCollection : ObservableCollection<VstParameterInfo>
    {
        /// <summary>
        /// Contructs an empty instance.
        /// </summary>
        public VstParameterInfoCollection()
        { }

        /// <summary>
        /// Constructs a prefilled instance.
        /// </summary>
        /// <param name="parameterInfos">Must not be null.</param>
        public VstParameterInfoCollection(IEnumerable<VstParameterInfo> parameterInfos)
            : base(parameterInfos)
        { }

        /// <summary>
        /// Adds a range of <see cref="VstParameterInfo"/> instances to the collection.
        /// </summary>
        /// <param name="parameterInfos">Must not be null.</param>
        public void AddRange(IEnumerable<VstParameterInfo> parameterInfos)
        {
            Throw.IfArgumentIsNull(parameterInfos, nameof(parameterInfos));

            foreach (VstParameterInfo paramInfo in parameterInfos)
            {
                Add(paramInfo);
            }
        }
    }
}
