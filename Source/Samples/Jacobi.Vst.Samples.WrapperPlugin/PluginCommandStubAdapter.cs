using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Host.Interop;
using Microsoft.Extensions.Configuration;
using System;

namespace Jacobi.Vst.Samples.WrapperPlugin
{
    public class PluginCommandStubAdapter : IVstPluginCommandStub
    {
        private VstPluginContext _pluginCtx;

        #region IVstPluginCommandStub Members

        public VstPluginInfo GetPluginInfo(IVstHostCommandProxy hostCmdProxy)
        {
            //
            // get the path to the wrapped plugin from config
            //

            if (PluginConfiguration == null)
            {
                throw new InvalidOperationException("No plugin configuration found.");
            }

            var pluginPath = PluginConfiguration["PluginPath"];

            Host.HostCommandStubAdapter hostCmdAdapter = new Host.HostCommandStubAdapter(hostCmdProxy);
            _pluginCtx = VstPluginContext.Create(pluginPath, hostCmdAdapter);

            return _pluginCtx.PluginInfo;
        }

        public IConfiguration PluginConfiguration { get; set; }

        // Change this to intercept the plugin commands.
        public IVstPluginCommands24 Commands { get { return _pluginCtx.PluginCommandStub.Commands; } }

        #endregion
    }
}
