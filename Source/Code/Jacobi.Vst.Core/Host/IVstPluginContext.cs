using System;
using System.ComponentModel;

using Jacobi.Vst.Core.Plugin;

namespace Jacobi.Vst.Core.Host
{
    public interface IVstPluginContext : INotifyPropertyChanged
    {
        void Set<T>(string keyName, T value);
        T Find<T>(string keyName);
        void Remove(string keyName);

        IVstHostCommandStub HostCommandStub { get; }
        IVstPluginCommandStub PluginCommandStub { get; }

        VstPluginInfo PluginInfo { get; }
        void AcceptPluginInfoData(bool raiseEvents);
    }
}
