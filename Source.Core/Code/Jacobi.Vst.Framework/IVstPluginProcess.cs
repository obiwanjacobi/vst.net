namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface is implemented when the plugin whishes to receive pre and post <see cref="IVstPluginAudioProcessor.Process"/> calls.
    /// </summary>
    public interface IVstPluginProcess
    {
        /// <summary>
        /// Called just before Process is called.
        /// </summary>
        void Start();
        /// <summary>
        /// Called just after Process is called.
        /// </summary>
        void Stop();
    }
}
