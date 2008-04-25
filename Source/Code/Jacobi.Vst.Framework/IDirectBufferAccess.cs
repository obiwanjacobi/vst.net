namespace Jacobi.Vst.Framework
{
    public unsafe interface IDirectBufferAccess32
    {
        float* Buffer { get; }
    }

    public unsafe interface IDirectBufferAccess64
    {
        double* Buffer { get; }
    }
}
