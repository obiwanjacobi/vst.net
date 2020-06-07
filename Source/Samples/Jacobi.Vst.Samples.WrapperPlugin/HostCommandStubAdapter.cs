using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Plugin;

namespace Jacobi.Vst.Samples.WrapperPlugin.Host
{
    public class HostCommandStubAdapter : Jacobi.Vst.Core.Host.IVstHostCommandStub
    {
        readonly IVstHostCommandProxy _hostCmdProxy;

        public HostCommandStubAdapter(IVstHostCommandProxy hostCmdProxy)
        {
            _hostCmdProxy = hostCmdProxy;
        }

        #region IVstHostCommandStub Members

        public Jacobi.Vst.Core.Host.IVstPluginContext PluginContext
        { get; set; }

        // Change this to intercept commands
        public IVstHostCommands20 Commands
        {
            get { return _hostCmdProxy.Commands; }
        }

        #endregion
    }
}
