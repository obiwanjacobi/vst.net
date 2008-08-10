namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// This interface is implemented when the plugin whishes to reveal more about its connections.
    /// </summary>
    /// <remarks>This interface is still under construction.</remarks>
    public interface IVstPluginConnections
    {
        /// <summary>
        /// Gets or sets the input speaker arrangement.
        /// </summary>
        /// <remarks>Still under construction.</remarks>
        VstSpeakerArrangement InputSpeakerArrangement {get; set;}
        /// <summary>
        /// Gets or sets the output speaker arrangement.
        /// </summary>
        /// <remarks>Still under construction.</remarks>
        VstSpeakerArrangement OutputSpeakerArrangement { get; set; }
        /// <summary>
        /// Gets the collection of connection information for the inputs.
        /// </summary>
        /// <remarks>Still under construction.</remarks>
        VstConnectionInfoCollection InputConnectionInfos { get; }
        /// <summary>
        /// Gets the collection of connection information for the outputs.
        /// </summary>
        /// <remarks>Still under construction.</remarks>
        VstConnectionInfoCollection OutputConnectionInfos { get; }
    }
}
