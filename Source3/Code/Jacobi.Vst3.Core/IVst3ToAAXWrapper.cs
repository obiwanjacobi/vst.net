using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    // passed as context to IPluginBase::Initialize
    [ComImport]
    [Guid(Interfaces.IVst3ToAAXWrapper)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVst3ToAAXWrapper
    {
    }
}
