namespace Jacobi.Vst.Framework
{
    // is 'processor' the right word?
    /// <summary>
    /// Provides access to the Offline processing capabilies of the host.
    /// </summary>
    public interface IVstHostOfflineProcessor
    {
        void Start();
        void Read();
        void Write();
        void GetCurrentPass();
        void GetCurrentMetaPass();
    }
}
