namespace Jacobi.Vst.Core.Plugin
{
    using System;

    /// <summary>
    /// The Vst Host Stub called by the Plugin (Framework).
    /// </summary>
    public interface IVstHostCommandStub : IVstHostCommands20, IDisposable
    {
        bool IsInitialized();
        bool UpdatePluginInfo(VstPluginInfo pluginInfo);
    }
}
