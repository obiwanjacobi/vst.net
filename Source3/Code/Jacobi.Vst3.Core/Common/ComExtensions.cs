using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Jacobi.Vst3.Core.Common
{
    public static class ComExtensions
    {
        [SupportedOSPlatform("windows")]
        public static T AsObject<T>(this IntPtr unknownPtr) where T : class
            => Marshal.GetObjectForIUnknown(unknownPtr) as T;
    }
}
