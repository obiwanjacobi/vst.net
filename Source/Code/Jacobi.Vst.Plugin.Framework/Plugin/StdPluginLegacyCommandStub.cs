using Jacobi.Vst.Core;

namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    /// <summary>
    /// Implements the Plugin command stub containing the legacy methods.
    /// </summary>
    /// <remarks>Derive your public plugin command stub class from this class if you need to support older VST versions.
    /// Most methods are not implemented. You should override and implement the specific methods you need to support.</remarks>
    public abstract class StdPluginLegacyCommandStub : StdPluginCommandStub
    {
        protected override IVstPluginCommands24 CreatePluginCommands(VstPluginContext pluginCtx)
        {
            return new VstPluginCommandsLegacy(pluginCtx);
        }
    }
}
