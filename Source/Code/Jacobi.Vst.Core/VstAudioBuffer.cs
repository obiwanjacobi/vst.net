namespace Jacobi.Vst.Core
{
    public unsafe class VstAudioBuffer
    {
        public VstAudioBuffer(float* buffer, int length)
        {
            Buffer = buffer;
            Count = length;
        }

        public int Count { get; private set; }
        protected float* Buffer { get; private set; }
    }

    public unsafe class VstPrecisionAudioBuffer
    {
        public VstPrecisionAudioBuffer(double* buffer, int length)
        {
            Buffer = buffer;
            Count = length;
        }

        public int Count { get; private set; }
        protected double* Buffer { get; private set; }
    }
}
