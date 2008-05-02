namespace Jacobi.Vst.Framework
{
    public interface IVstPluginOfflineProcessor
    {
        int TotalSamplesToProcess { get; set; }
        void Notify();
        void Prepare();
        void Run();
        void ProcessVariableIO();
    }
}
