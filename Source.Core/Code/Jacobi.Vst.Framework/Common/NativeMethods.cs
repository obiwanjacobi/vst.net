namespace Jacobi.Vst.Framework.Common
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// P/Invoke native methods.
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
    }
}
