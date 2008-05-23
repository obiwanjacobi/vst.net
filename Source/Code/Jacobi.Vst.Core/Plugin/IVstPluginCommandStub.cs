namespace Jacobi.Vst.Core.Plugin
{
    public interface IVstPluginCommandStub : IVstPluginCommands24
    {
        VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub);
    }
}
