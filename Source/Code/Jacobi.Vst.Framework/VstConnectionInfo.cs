namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstConnectionInfo class represents information about a plugin connection pin.
    /// </summary>
    public class VstConnectionInfo
    {
        private string _label;
        /// <summary>
        /// Gets or sets the label for this connection pin.
        /// </summary>
        /// <remarks>The label cannot be more than 63 characters.</remarks>
        public string Label
        {
            get { return _label; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxLabelLength, "Label");

                _label = value;
            }
        }

        private string _shortLabel;
        /// <summary>
        /// Gets or sets the short label for the connection pin.
        /// </summary>
        /// <remarks>The short label cannot exceed 7 characters.</remarks>
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, "ShortLabel");

                _shortLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the speaker arrangement type.
        /// </summary>
        public VstSpeakerArrangementType SpeakerArrangementType { get; set; }
    }
}
