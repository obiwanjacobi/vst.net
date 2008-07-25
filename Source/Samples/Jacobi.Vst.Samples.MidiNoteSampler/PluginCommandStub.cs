namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Framework.Plugin;

    public sealed class PluginCommandStub : StdPluginCommandStub, Core.Plugin.IVstPluginCommandStub
    {
        protected override Jacobi.Vst.Framework.IVstPlugin CreatePluginInstance()
        {
            return new Plugin();
        }
    }
}
