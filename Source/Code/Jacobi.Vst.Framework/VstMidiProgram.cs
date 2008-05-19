namespace Jacobi.Vst.Framework
{
    using System;

    public class VstMidiProgram
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

        public byte ProgramChange { get; set; }
        public byte BankSelectMsb { get; set; }
        public byte BankSelectLsb { get; set; }

        public VstMidiCategory ParentCategory { get; set; }

        public virtual string GetKeyName(int keyNumber)
        {
            int note = keyNumber % 12;
            int octave = keyNumber / 12;

            return String.Format("{0}{1}", note, octave - 2);
        }
    }
}
