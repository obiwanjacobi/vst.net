using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    public static class Extensions
    {
        public static IParameterChanges GetInputParameterChanges(this ref ProcessData processData)
        {
            if (processData.InputParameterChangesPtr == IntPtr.Zero) return null;
            var unknown = Marshal.GetObjectForIUnknown(processData.InputParameterChangesPtr);
            return unknown as IParameterChanges;
        }

        public static ProcessContext GetProcessContext(this ref ProcessData processData)
        {
            return Marshal.PtrToStructure<ProcessContext>(processData.ProcessContext);
            //return (ProcessContext)processData.ProcessContext.ToPointer();
        }
    }
}
