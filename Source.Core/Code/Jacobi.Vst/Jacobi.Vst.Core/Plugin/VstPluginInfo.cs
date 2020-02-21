namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// Plugin information passed to the Host.
    /// </summary>
    public class VstPluginInfo
    {
        /// <summary>
        /// Plugin flags.
        /// </summary>
        public VstPluginFlags Flags { get; set; }

        /// <summary>
        /// The number of programs the plugin supports.
        /// </summary>
        public int ProgramCount { get; set; }

        /// <summary>
        /// The number of parameters the plugin supports.
        /// </summary>
        public int ParameterCount { get; set; }

        /// <summary>
        /// The number of audio inputs the plugin supports.
        /// </summary>
        public int AudioInputCount { get; set; }

        /// <summary>
        /// The number of audio outputs the plugin supports.
        /// </summary>
        public int AudioOutputCount { get; set; }

        /// <summary>
        /// The latency of the plugin audio processing.
        /// </summary>
        public int InitialDelay { get; set; }

        /// <summary>
        /// The unique ID of the plugin.
        /// </summary>
        /// <remarks>Must be a four character code.</remarks>
        public int PluginID { get; set; }

        /// <summary>
        /// The version of the plugin.
        /// </summary>
        public int PluginVersion { get; set; }
    }
}
