namespace Jacobi.Vst.Framework
{
    public class VstMidiProgram
    {
        public string Name;
        public byte ProgramChange;
        public byte BankSelectMsb;
        public byte BankSelectLsb;

        public VstMidiCategory ParentCategory;

        public string GetKeyName(int keyNumber)
        {
            return null;
        }
    }
}
