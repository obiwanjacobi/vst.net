namespace Jacobi.Vst.Framework
{
    public interface IVstAudioChannel
    {
        bool CanWrite { get; }
        int SampleCount { get; }
        float this[int index] { get; set; }
    }
}
