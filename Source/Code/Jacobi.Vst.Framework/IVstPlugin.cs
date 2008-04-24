namespace Jacobi.Vst.Framework
{
    using System;

    public interface IVstPlugin : IExtensibleObject, IDisposable
    {
        ProductInfo ProductInfo { get; }
        string BaseDirectory { get;}
        string Name { get;}
        int Category { get;} // return enum
        int Capabilities { get;} // return enum
        VstParameterCollection Parameters { get;}

        void Open();
        void Close();
        void Suspend();
        void Resume();
    }
}
