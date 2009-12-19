namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Framework.Plugin;

    /// <summary>
    /// The public Plugin Command Stub implementation derived from the framework provided <see cref="StdPluginCommandStub"/>.
    /// </summary>
    public sealed class PluginCommandStub : StdPluginDeprecatedCommandStub, Core.Plugin.IVstPluginCommandStub
    {
        /// <summary>
        /// Called by the framework to create the plugin root class.
        /// </summary>
        /// <returns>Never returns null.</returns>
        protected override Jacobi.Vst.Framework.IVstPlugin CreatePluginInstance()
        {
            return new Plugin();
        }
    }
}
