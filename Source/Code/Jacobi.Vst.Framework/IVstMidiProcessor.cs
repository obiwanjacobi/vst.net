namespace Jacobi.Vst.Framework
{
    using System;

    public interface IVstMidiProcessor : IDisposable
    {
        void Process(VstEventCollection events);
    }
}
