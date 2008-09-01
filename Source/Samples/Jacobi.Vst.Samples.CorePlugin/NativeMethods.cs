namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Container class for native P/Invoke methods.
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
    }
}
