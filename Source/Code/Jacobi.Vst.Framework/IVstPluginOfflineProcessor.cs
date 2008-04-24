namespace Jacobi.Vst.Framework
{
    public interface IVstPluginOfflineProcessor
    {
        void Notify();
        void Prepare();
        void Run();
    }
}
