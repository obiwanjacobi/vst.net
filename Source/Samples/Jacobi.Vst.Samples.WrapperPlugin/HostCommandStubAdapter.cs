using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Host.Interop;

namespace Jacobi.Vst.Samples.WrapperPlugin.Host
{
    public class HostCommandStubAdapter : IVstHostCommandStub
    {
        // hot-reload plugin info (cannot do much on its own)
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

        public IVstPluginCommands24? OnLoadPlugin(string pluginPath)
        {
            LoadPlugin(pluginPath);
            return PluginContext?.PluginCommandStub.Commands;
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
            LoadedPluginContext = VstPluginContext.Create(pluginPath, this);

            // update plugin info to loaded plugin
            _hostCmdProxy.UpdatePluginInfo(LoadedPluginContext.PluginInfo);
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
