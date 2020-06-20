using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IAutomationState)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAutomationState
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetAutomationState(
            [MarshalAs(UnmanagedType.I4), In] AutomationStates state);
    }

    public enum AutomationStates
    {
        NoAutomation = 0,      // Not Read and not Write
        ReadState = 1 << 0,    // Read state
        WriteState = 1 << 1,   // Write state

        ReadWriteState = ReadState | WriteState, // Read and Write enable
    };
}
