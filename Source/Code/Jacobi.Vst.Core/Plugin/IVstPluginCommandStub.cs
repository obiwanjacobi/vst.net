namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// The Plugin Command Stub called by the Interop.
    /// </summary>
    public interface IVstPluginCommandStub : IVstPluginCommands24
    {
        VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub);
    }
}
