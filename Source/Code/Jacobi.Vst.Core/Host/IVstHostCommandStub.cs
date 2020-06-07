namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The IVstHostCommandStub interface is implemented by the command stub for handling 
    /// incoming Host commands from the Plugin at the interop assembly.
    /// </summary>
    public interface IVstHostCommandStub
    {
        /// <summary>
        /// Gets or sets the plugin context this instance is part of.
        /// </summary>
        IVstPluginContext PluginContext { get; set; }

        /// <summary>
        /// All Host Commands.
        /// </summary>
        IVstHostCommands20 Commands { get; }
    }
}
