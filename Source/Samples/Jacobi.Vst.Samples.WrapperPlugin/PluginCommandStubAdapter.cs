using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Plugin;
using Microsoft.Extensions.Configuration;

namespace Jacobi.Vst.Samples.WrapperPlugin;

public class PluginCommandStubAdapter : IVstPluginCommandStub
{
    private readonly PluginCommands _commands = new PluginCommands();
    private Host.HostCommandStubAdapter? _hostAdapter;

    #region IVstPluginCommandStub Members

    public VstPluginInfo GetPluginInfo(IVstHostCommandProxy hostCmdProxy)
    {
        _hostAdapter = new Host.HostCommandStubAdapter(hostCmdProxy);
        _commands.OnLoadPlugin = _hostAdapter.OnLoadPlugin;
        return _hostAdapter.PluginInfo;
    }

    public IConfiguration? PluginConfiguration { get; set; }

    public IVstPluginCommands24 Commands
    {
        get { return _commands; }
    }

    #endregion
}
