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

        private string _label;
        /// <summary>
        /// The name or label of the connection pin.
        /// </summary>
        /// <remarks>The value must not exceed 64 characters.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 63 characters.</exception>
        public string Label
        {
            get { return _label; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxLabelLength, "Label");

                _label = value;
            }
        }

        private string _shortLabel;
        /// <summary>
        /// The short label of the connection pin.
        /// </summary>
        /// <remarks>The value must not exceed 8 characters.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 7 characters.</exception>
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxShortLabelLength, "ShortLabel");

                _shortLabel = value;
            }
        }

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
