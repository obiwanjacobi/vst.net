namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Common;
    using System;

    /// <summary>
    /// The VstConnectionInfo class represents information about a plugin connection pin.
    /// </summary>
    public class VstConnectionInfo : ObservableObject
    {
        private string _label = String.Empty;
        /// <summary>
        /// Gets or sets the label for this connection pin.
        /// </summary>
        /// <remarks>The label cannot be more than 64 characters.</remarks>
        public string Label
        {
            get { return _label; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxLabelLength, nameof(Label));

                SetProperty(value, ref _label, nameof(Label));
            }
        }

        private string _shortLabel = String.Empty;
        /// <summary>
        /// Gets or sets the short label for the connection pin.
        /// </summary>
        /// <remarks>The short label cannot exceed 8 characters.</remarks>
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, nameof(ShortLabel));

                SetProperty(value, ref _shortLabel, nameof(ShortLabel));
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
                SetProperty(value, ref _speakerArrangementType, nameof(SpeakerArrangementType));
            }
        }
    }
}
