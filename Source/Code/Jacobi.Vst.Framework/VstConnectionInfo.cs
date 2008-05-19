namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public class VstConnectionInfo
    {
        private string _label;
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxLabelLength, "Label");

                _label = value;
            }
        }

        private string _shortLabel;
        public string ShortLabel
        {
            get
            {
                return _shortLabel;
            }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, "ShortLabel");

                _shortLabel = value;
            }
        }

        public VstSpeakerArrangementType SpeakerArrangementType { get; set; }
    }
}
