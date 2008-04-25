namespace Jacobi.Vst.Framework
{
    public interface IVstMidiProcessor
    {
        void Process(VstEventCollection events);
    }
}
