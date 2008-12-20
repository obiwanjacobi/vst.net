using System;

namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The IVstHostCommandStub interface is implemented by the command stub for handling 
    /// incoming Host commands from the Plugin at the interop assmebly.
    /// </summary>
    /// <remarks>The interface derives from <see cref="IVstHostCommands20"/>.</remarks>
    public interface IVstHostCommandStub : IVstHostCommands20
    {
        IVstPluginContext PluginContext { get; set; }
    }
}
