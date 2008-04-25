namespace Jacobi.Vst.Framework
{
    public interface IVstAudioPrecisionChannel
    {
        bool CanWrite { get; }
        int SampleCount { get; }
        double this[int index] { get; set; }
    }
}
