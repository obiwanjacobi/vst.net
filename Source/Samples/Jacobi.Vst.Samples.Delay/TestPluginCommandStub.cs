namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;

    public class TestPluginCommandStub : StdPluginCommandStub, Core.Plugin.IVstPluginCommandStub
    {
        protected override IVstPlugin CreatePluginInstance()
        {
            return new FxTestPlugin();
        }
    }
}
