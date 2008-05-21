namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public interface IVstPlugin : IExtensible, IDisposable
    {
        VstProductInfo ProductInfo { get; }
        string Name { get;}

        VstPluginCategory Category { get; }
        VstPluginCapabilities Capabilities { get; }
        int InitialDelay { get; }
        int PluginID { get; }

        void Open(IVstHost host);
        void Suspend();
        void Resume();
    }
}
