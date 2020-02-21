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
        /// Called by the host to propose a new speaker arrangement.
        /// </summary>
        /// <param name="input">Must not be null.</param>
        /// <param name="output">Must not be null.</param>
        /// <returns>Returns true when the plugin accepts the proposed arrangements.</returns>
        bool AcceptNewArrangement(VstSpeakerArrangement input, VstSpeakerArrangement output);
        /// <summary>
        /// Gets the input speaker arrangement.
        /// </summary>
        /// <remarks>Still under construction.</remarks>
        VstSpeakerArrangement InputSpeakerArrangement { get; }
        /// <summary>
        /// Gets the output speaker arrangement.
        /// </summary>
        /// <remarks>Still under construction.</remarks>
        VstSpeakerArrangement OutputSpeakerArrangement { get; }
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
