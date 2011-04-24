namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// The VstConnectionInfo class represents information about a plugin connection pin.
    /// </summary>
    public class VstConnectionInfo : ObservableObject
    {
        /// <summary>Label</summary>
        public const string LabelPropertyName = "Label";
        /// <summary>ShortLabel</summary>
        public const string ShortLabelPropertyName = "ShortLabel";
        /// <summary>SpeakerArrangementType</summary>
        public const string SpeakerArrangementTypePropertyName = "SpeakerArrangementType";

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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxLabelLength, LabelPropertyName);

                SetProperty(value, ref _label, LabelPropertyName);
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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, ShortLabelPropertyName);

                SetProperty(value, ref _shortLabel, ShortLabelPropertyName);
            }
        }

        private VstSpeakerArrangementType _speakerArrangementType;
        /// <summary>
        /// Gets or sets the speaker arrangement type.
        /// </summary>
        public VstSpeakerArrangementType SpeakerArrangementType
        {
            get { return _speakerArrangementType; }
            set
            {
                SetProperty(value, ref _speakerArrangementType, SpeakerArrangementTypePropertyName);
            }
        }
    }
}
