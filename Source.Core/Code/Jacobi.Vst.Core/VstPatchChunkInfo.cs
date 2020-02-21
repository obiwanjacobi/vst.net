namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Used to communicate the version of the chunk data before the plugin is asked to load it in.
    /// </summary>
    public class VstPatchChunkInfo
    {
        /// <summary>
        /// Constructs an immutable instance.
        /// </summary>
        /// <param name="version">Version number of the format. Should be 1.</param>
        /// <param name="pluginId">The unique ID of the plugin that wrote the chunk data.</param>
        /// <param name="pluginVersion">The version of the plugin that wrote the chunk data.</param>
        /// <param name="elementCount">The number of Programs (Bank) or Parameters (Program).</param>
        public VstPatchChunkInfo(int version, int pluginId, int pluginVersion, int elementCount)
        {
            Version = version;
            PluginID = pluginId;
            PluginVersion = pluginVersion;
            ElementCount = elementCount;
        }

        /// <summary>
        /// Gets the format version (should be 1).
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Gets the unique plugin ID.
        /// </summary>
        public int PluginID { get; private set; }

        /// <summary>
        /// Gets the plugin version.
        /// </summary>
        public int PluginVersion { get; private set; }

        /// <summary>
        /// Gets the number of Programs (Bank) or Parameters (Program).
        /// </summary>
        public int ElementCount { get; private set; }
    }
}
