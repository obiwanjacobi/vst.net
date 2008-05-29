namespace Jacobi.Vst.Core
{
    public class VstPatchChunkInfo
    {
        public VstPatchChunkInfo(int version, int pluginId, int pluginVersion, int elementCount)
        {
            Version = version;
            PluginID = pluginId;
            PluginVersion = pluginVersion;
            ElementCount = elementCount;
        }

        public int Version { get; private set; }
        public int PluginID { get; private set; }
        public int PluginVersion { get; private set; }
        public int ElementCount { get; private set; }
    }
}
