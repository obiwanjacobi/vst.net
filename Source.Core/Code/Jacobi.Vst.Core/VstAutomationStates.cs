namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Reports the level of parameter automation support by the host.
    /// </summary>
    public enum VstAutomationStates
    {
        /// <summary>not supported by Host</summary>
        Unsupported,
        /// <summary>off</summary>
        Off,
        /// <summary>read</summary>
        Read,
        /// <summary>write</summary>
        Write,
        /// <summary>read and write</summary>
        ReadWrite
    }
}
