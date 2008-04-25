namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public interface IVstPlugin : IExtensibleObject, IDisposable
    {
        ProductInfo ProductInfo { get; }
        string BaseDirectory { get;}
        string Name { get;}

        VstPluginCategory Category { get; }
        VstPluginCapabilities Capabilities { get; }
        int InitialDelay { get; }
        int PluginID { get; }

        void Open(IVstHost host);
        //void Close(); // call Dispose
        void Suspend();
        void Resume();
    }
}
