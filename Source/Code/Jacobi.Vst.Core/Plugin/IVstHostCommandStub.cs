namespace Jacobi.Vst.Core.Plugin
{
    using System;

    public interface IVstHostCommandStub : IVstHostCommands20, IDisposable
    {
        bool IsInitialized();
        bool UpdatePluginInfo(VstPluginInfo pluginInfo);
    }
}
