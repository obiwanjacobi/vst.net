namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// Plugin information passed to the Host.
    /// </summary>
    public class VstPluginInfo
    {
        public VstPluginFlags Flags;
        public int NumberOfPrograms;
        public int NumberOfParameters;
        public int NumberOfAudioInputs;
        public int NumberOfAudioOutputs;
        public int InitialDelay;
        public int PluginID;
        public int PluginVersion;
    }
}
