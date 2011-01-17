using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// This object receives all calls from the (unmanaged) host.
    /// </summary>
    /// <remarks>
    /// An instance of this object is created automatically by the Jacobi.Vst.Interop assembly
    /// when the plugin is loaded into the host. Interop marshals all calls from unmanaged C++
    /// to this object.
    /// </remarks>
    public class PluginCommandStub : StdPluginDeprecatedCommandStub, IVstPluginCommandStub
    {
        /// <summary>
        /// Returns an instance of the VST.NET Plugin root object.
        /// </summary>
        /// <returns>Must never return null.</returns>
        protected override IVstPlugin CreatePluginInstance()
        {
            return new Plugin();
        }
    }
}
