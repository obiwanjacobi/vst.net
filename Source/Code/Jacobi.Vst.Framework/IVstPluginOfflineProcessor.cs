namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface is implemented when a plugin supports offline processing.
    /// </summary>
    /// <remarks>This interface is still under construction.</remarks>
    public interface IVstPluginOfflineProcessor
    {
        /// <summary>
        /// Under construction.
        /// </summary>
        int TotalSamplesToProcess { get; set; }
        /// <summary>
        /// Under construction.
        /// </summary>
        void Notify();
        /// <summary>
        /// Under construction.
        /// </summary>
        void Prepare();
        /// <summary>
        /// Under construction.
        /// </summary>
        void Run();
        /// <summary>
        /// Under construction.
        /// </summary>
        void ProcessVariableIO();
    }
}
