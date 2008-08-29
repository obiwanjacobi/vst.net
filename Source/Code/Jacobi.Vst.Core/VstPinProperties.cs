namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Communicates connection pin properties of the plugin to host.
    /// </summary>
    public class VstPinProperties
    {
        /// <summary>
        /// The flags of the connection pin.
        /// </summary>
        public VstPinPropertiesFlags Flags { get; set; }

        /// <summary>
        /// The name or label of the connection pin.
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// The short label of the connection pin.
        /// </summary>
        public string ShortLabel { get; set; }

        /// <summary>
        /// The speaker arrangement used for this connection pin.
        /// </summary>
        public VstSpeakerArrangementType ArrangementType { get; set; }
    }

    /// <summary>
    /// Flags for the pin properties.
    /// </summary>
    [Flags]
    public enum VstPinPropertiesFlags
    {
        /// <summary>The pin is active, ignored by Host.</summary>
        PinIsActive = 1 << 0,
        /// <summary>The pin is first of a stereo pair.</summary>
        PinIsStereo = 1 << 1,
        /// <summary>The <see cref="VstPinProperties.ArrangementType"/> is valid and can be used to get the wanted arrangement.</summary>
        PinUseSpeaker = 1 << 2
    };
}
