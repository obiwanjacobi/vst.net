namespace Jacobi.Vst.Framework
{
    // is 'processor' the right word?
    public interface IVstHostOfflineProcessor
    {
        void Start();
        void Read();
        void Write();
        void GetCurrentPass();
        void GetCurrentMetaPass();
    }
}
