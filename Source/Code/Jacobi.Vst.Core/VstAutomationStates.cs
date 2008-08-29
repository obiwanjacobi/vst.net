namespace Jacobi.Vst.Core
{
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
