namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// Represents the root interface for (communicating with) the vst host.
    /// </summary>
    /// <remarks>
    /// The IVstHost interface derives from IExtensible and thus serves as 
    /// a root for querying for other interfaces.
    /// </remarks>
    public interface IVstHost : IExtensible
    {
        /// <summary>
        /// Gets the product information of the vst host.
        /// </summary>
        VstProductInfo ProductInfo { get; }

        /// <summary>
        /// Gets the host capabilities.
        /// </summary>
        VstHostCapabilities Capabilities { get; }

        /// <summary>
        /// Gets the current process level (from what thread the plugin is called).
        /// </summary>
        VstProcessLevels ProcessLevel { get; }
    }
}
