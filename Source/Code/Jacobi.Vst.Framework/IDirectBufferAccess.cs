namespace Jacobi.Vst.Framework
{
    public unsafe interface IDirectBufferAccess32
    {
        int SampleCount { get; }
        float* Buffer { get; }
    }

    public unsafe interface IDirectBufferAccess64
    {
        int SampleCount { get; }
        double* Buffer { get; }
    }
}
