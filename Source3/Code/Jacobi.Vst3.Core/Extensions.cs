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
            return processData.InputParameterChanges.Cast<IParameterChanges>();
        }

        public static IParameterChanges GetOutputParameterChanges(this ref ProcessData processData)
        {
            return processData.OutputParameterChanges.Cast<IParameterChanges>();
        }

        public static IEventList GetInputEvents(this ref ProcessData processData)
        {
            return processData.InputEvents.Cast<IEventList>();
        }

        public static IEventList GetOutputEvents(this ref ProcessData processData)
        {
            return processData.OutputEvents.Cast<IEventList>();
        }

        public static bool TryGetProcessContext(this ref ProcessData processData, ref ProcessContext processContext)
        {
            if (processData.ProcessContext == IntPtr.Zero) return false;
            Marshal.PtrToStructure(processData.ProcessContext, processContext);
            return true;
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

        public static T Crop<T>(this T value, T min, T max)
            where T : IComparable<T>
        {
            if (value.CompareTo(max) > 0)
            {
                value = max;
            }

            if (value.CompareTo(min) < 0)
            {
                value = min;
            }

            return value;
        }
    }
}
