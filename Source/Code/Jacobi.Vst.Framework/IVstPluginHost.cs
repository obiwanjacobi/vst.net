namespace Jacobi.Vst.Framework
{
    using System.Collections.Generic;

    // implemented by a plugin that, in turn, hosts plugins
    public interface IVstPluginHost : IEnumerable<VstPluginInfo>
    {
        // no members
    }

    public struct VstPluginInfo
    {
        public int PluginId;
        public string PluginName;
    }
}
