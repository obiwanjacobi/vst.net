namespace Jacobi.Vst.Core
{
    public enum VstAutomationStates
    {
        Unsupported = 0,	// not supported by Host
        Off,				// off
        Read,				// read
        Write,			    // write
        ReadWrite			// read and write
    }
}
