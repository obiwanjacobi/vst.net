using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jacobi.Vst.Plugin.Framework
{
    /// <summary>
    /// Manages a collection of <see cref="VstConnectionInfo"/> instances.
    /// </summary>
    public class VstConnectionInfoCollection : ObservableCollection<VstConnectionInfo>
    {
        /// <summary>
        /// Contructs an empty instance.
        /// </summary>
        public VstConnectionInfoCollection()
        { }

        /// <summary>
        /// Constructs a prefilled instance.
        /// </summary>
        /// <param name="connectionInfos">Must not be null.</param>
        public VstConnectionInfoCollection(IEnumerable<VstConnectionInfo> connectionInfos)
            : base(connectionInfos)
        { }
    }
}
