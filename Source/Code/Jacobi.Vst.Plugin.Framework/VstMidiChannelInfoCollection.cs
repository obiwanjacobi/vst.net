using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jacobi.Vst.Plugin.Framework
{
    /// <summary>
    /// Manages a collection of <see cref="VstMidiChannelInfo"/> instances.
    /// </summary>
    public class VstMidiChannelInfoCollection : ObservableCollection<VstMidiChannelInfo>
    {
        /// <summary>
        /// Contructs an empty instance.
        /// </summary>
        public VstMidiChannelInfoCollection()
        { }

        /// <summary>
        /// Constructs a prefilled instance.
        /// </summary>
        /// <param name="channelInfos">Must not be null.</param>
        public VstMidiChannelInfoCollection(IEnumerable<VstMidiChannelInfo> channelInfos)
            : base(channelInfos)
        { }
    }
}
