namespace Jacobi.Vst.Framework
{
    public interface IVstHostSequencer : IExtensibleObject
    {
        object GetTime(); // return struct
        long SampleRate { get;}
        long BlockSize { get;}
        long InputLatency { get;}
        long OutputLatency { get;}
        int ProcessLevel { get;} // return enum
        int Capabilities { get;} // return enum

        bool UpdatePluginIO();
    }
}
