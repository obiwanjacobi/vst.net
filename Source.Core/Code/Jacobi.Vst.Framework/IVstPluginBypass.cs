namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface is implemented by plugins that allow the host to by pass them.
    /// </summary>
    /// <remarks>Bypassing can mean not interpret incoming midi or passing incoming audio to the output.</remarks>
    public interface IVstPluginBypass
    {
        /// <summary>
        /// Gets or sets the bypass status.
        /// </summary>
        /// <remarks>The host's call to bypass the plugin ends up here when the interface is implemented by the plugin.</remarks>
        bool Bypass { get;set;}
    }
}
