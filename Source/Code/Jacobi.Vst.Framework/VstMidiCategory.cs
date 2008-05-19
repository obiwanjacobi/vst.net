namespace Jacobi.Vst.Framework
{
    public class VstMidiCategory
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxMidiNameLength, "Name");

                _name = value;
            }
        }

        public VstMidiCategory ParentCategory { get; set; }
    }
}
