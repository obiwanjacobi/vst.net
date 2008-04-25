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
        public float* Buffer { get; private set; }
    }

    public unsafe class VstAudioPrecisionBuffer
    {
        public VstAudioPrecisionBuffer(double* buffer, int length)
        {
            Buffer = buffer;
            Count = length;
        }

        public int Count { get; private set; }
        public double* Buffer { get; private set; }
    }
}
