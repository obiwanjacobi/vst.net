using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IDependent)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDependent
    {
        void Update(
            [MarshalAs(UnmanagedType.IUnknown), In] Object changedUnknown,
            [MarshalAs(UnmanagedType.I4), In] Int32 message);
    }

    // IDependent.Update messages
    public enum ChangeMessages
    {
        kWillChange,
        kChanged,
        kDestroyed,
        kWillDestroy,

        kStdChangeMessageLast = kWillDestroy
    }
}
