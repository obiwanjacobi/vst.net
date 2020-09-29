using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;

namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    /// <summary>
    /// The public Plugin Command Stub implementation derived from the framework provided <see cref="StdPluginCommandStub"/>.
    /// </summary>
    public sealed class PluginCommandStub : StdPluginCommandStub
    {
        /// <summary>
        /// Called by the framework to create the plugin root object.
        /// </summary>
        /// <returns>Never returns null.</returns>
        protected override IVstPlugin CreatePluginInstance()
        {
            return new Plugin();
        }
    }
}
