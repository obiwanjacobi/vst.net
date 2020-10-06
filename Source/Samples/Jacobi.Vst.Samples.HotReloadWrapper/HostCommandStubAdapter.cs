using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Host.Interop;

namespace Jacobi.Vst.Samples.HotReloadWrapper.Host
{
    public class HostCommandStubAdapter : IVstHostCommandStub
    {
        private readonly VstPluginInfo _pluginInfo = new VstPluginInfo
        {
            Flags = VstPluginFlags.HasEditor | VstPluginFlags.CanReplacing,
            PluginID = 0x42424242,
            PluginVersion = 2000
        };

        private readonly IVstHostCommandProxy _hostCmdProxy;

        public HostCommandStubAdapter(IVstHostCommandProxy hostCmdProxy)
        {
            _hostCmdProxy = hostCmdProxy;
        }

        public IVstPluginCommands24 OnLoadPlugin(string pluginPath)
        {
            LoadPlugin(pluginPath);
            return PluginContext!.PluginCommandStub.Commands;
        }

        public VstPluginInfo PluginInfo
        {
            get
            {
                if (PluginContext != null)
                {
                    return PluginContext.PluginInfo;
                }
                return _pluginInfo;
            }
        }

        public VstPluginContext? LoadedPluginContext { get; private set; }

        public void LoadPlugin(string pluginPath)
        {
            UnloadPlugin();
            LoadedPluginContext = VstPluginContext.Create(pluginPath, this);

            _hostCmdProxy.UpdatePluginInfo(PluginInfo);
        }

        public void UnloadPlugin()
        {
            if (LoadedPluginContext != null)
            {
                LoadedPluginContext.Dispose();
                LoadedPluginContext = null;
                PluginContext = null;

                _hostCmdProxy.UpdatePluginInfo(PluginInfo);
            }
        }

        #region IVstHostCommandStub Members

        public IVstPluginContext? PluginContext { get; set; }

        public IVstHostCommands20 Commands
        {
            get { return _hostCmdProxy.Commands; }
        }

        #endregion
    }
}
