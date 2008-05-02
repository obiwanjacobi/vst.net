namespace Jacobi.Vst.Framework.TestPlugin
{
    using Jacobi.Vst.Framework;

    public class TestPluginCommandStub : Plugin.StdPluginCommandStub, Core.IVstPluginCommandStub
    {
        protected override IVstPlugin CreatePluginInstance()
        {
            return new FxTestPlugin();
        }
    }
}
