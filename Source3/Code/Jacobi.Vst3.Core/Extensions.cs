using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    public static class Extensions
    {
        //
        // ProcessData
        //

        public static IParameterChanges GetInputParameterChanges(this ref ProcessData processData)
        {
            return processData.InputParameterChangesPtr.Cast<IParameterChanges>();
        }

        public static ProcessContext GetProcessContext(this ref ProcessData processData)
        {
            return Marshal.PtrToStructure<ProcessContext>(processData.ProcessContext);
            //return (ProcessContext)processData.ProcessContext.ToPointer();
        }

        //
        // IParameterChanges
        //

        /// <summary>
        /// Calls IParameterChanges.GetParameterData(index).
        /// </summary>
        /// <returns>Can return null.</returns>
        public static IParamValueQueue GetParameterValue(this IParameterChanges parameterChanges, Int32 index)
        {
            if (parameterChanges == null) return null;
            return parameterChanges.GetParameterData(index).Cast<IParamValueQueue>();
        }

        /// <summary>
        /// Calls IParameterChanges.AddParameterData(id, index).
        /// </summary>
        /// <returns>Can return null.</returns>
        public static IParamValueQueue AddParameterValue(this IParameterChanges parameterChanges, UInt32 id, ref Int32 index)
        {
            if (parameterChanges == null) return null;
            return parameterChanges.AddParameterData(id, ref index).Cast<IParamValueQueue>();
        }

        //
        // IntPtr (unknown)
        //

        public static T Cast<T>(this IntPtr unknownPtr) where T : class
        {
            if (unknownPtr == IntPtr.Zero) return null;
            var unknown = Marshal.GetObjectForIUnknown(unknownPtr);
            return unknown as T;
        }
    }
}
