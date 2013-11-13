using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    // passed as context to IPluginBase::Initialize
    [ComImport]
    [Guid(Interfaces.IVst3ToVst2Wrapper)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVst3ToVst2Wrapper
    {
    }
}
