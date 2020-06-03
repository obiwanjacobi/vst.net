namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The IVstHostCommandStub interface is implemented by the command stub for handling 
    /// incoming Host commands from the Plugin at the interop assembly.
    /// </summary>
    /// <remarks>The interface derives from <see cref="IVstHostCommands20"/>.</remarks>
    public interface IVstHostCommandStub : IVstHostCommands20
    {
        /// <summary>
        /// Gets or sets the plugin context this instance is part of.
        /// </summary>
        IVstPluginContext PluginContext { get; set; }
    }
}
