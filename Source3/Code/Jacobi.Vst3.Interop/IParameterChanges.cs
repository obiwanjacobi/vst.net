using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IParameterChanges)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IParameterChanges
    {
    }
}
