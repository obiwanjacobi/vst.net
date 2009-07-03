namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface contains the deprecated (since VST 2.4) methods of the host.
    /// </summary>
    public interface IVstHostDeprecated
    {
        /// <summary>
        /// Indicates to the host that the Plugin wants to process Midi events.
        /// </summary>
        /// <returns>Returns true when the call was delivered to the Host.</returns>
        bool WantMidi();
    }
}
