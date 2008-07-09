namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core.Plugin;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;

    public class PluginCommandStub : StdPluginCommandStub, IVstPluginCommandStub
    {
        protected override IVstPlugin CreatePluginInstance()
        {
            return new Plugin();
        }
    }
}
