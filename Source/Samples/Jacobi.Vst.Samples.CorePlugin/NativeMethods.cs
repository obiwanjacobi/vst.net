namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
    }
}
