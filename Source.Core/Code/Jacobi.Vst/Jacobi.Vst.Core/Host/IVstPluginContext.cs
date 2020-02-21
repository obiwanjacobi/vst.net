using System;
using System.ComponentModel;

using Jacobi.Vst.Core.Plugin;

namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The IVstPluginContext interface represents the Plugin Context interface that is shared to other objects.
    /// </summary>
    public interface IVstPluginContext : INotifyPropertyChanged
    {
        /// <summary>
        /// Sets a context property identified by <paramref name="keyName"/> to a new <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Inferred, no need to specify it explicitly.</typeparam>
        /// <param name="keyName">The unique identification of the context property.</param>
        /// <param name="value">The new value to store in the context for the context property.</param>
        /// <remarks>The <see cref="INotifyPropertyChanged.PropertyChanged"/> event is raised when the property value is overwritten.
        /// If the property did not exist yet, it is created but no event is raised.</remarks>
        void Set<T>(string keyName, T value);

        /// <summary>
        /// Retrieves the value of a context property identified by <paramref name="keyName"/>.
        /// </summary>
        /// <typeparam name="T">Specifies the data type of the context property identified by <paramref name="keyName"/>.</typeparam>
        /// <param name="keyName">The unique identification of the context property.</param>
        /// <returns>Returns null if the property was not found.</returns>
        T Find<T>(string keyName);

        /// <summary>
        /// Removes the property identified by <paramref name="keyName"/> from the context.
        /// </summary>
        /// <param name="keyName">The unique identification of the context property.</param>
        void Remove(string keyName);

        /// <summary>
        /// Removes the property identified by <paramref name="keyName"/> from the context and
        /// calls <see cref="IDisposable.Dispose"/> if the data 'value' implements it.
        /// </summary>
        /// <param name="keyName">The unique identification of the context property.</param>
        void Delete(string keyName);

        /// <summary>
        /// Gets a reference to the HostCommandStub.
        /// </summary>
        IVstHostCommandStub HostCommandStub { get; }

        /// <summary>
        /// Gets a reference to the PluginCommandStub.
        /// </summary>
        IVstPluginCommandStub PluginCommandStub { get; }

        /// <summary>
        /// Gets a reference to the plug information.
        /// </summary>
        VstPluginInfo PluginInfo { get; set; }

        /// <summary>
        /// Promotes the plugin information published in the <b>AEffect</b> structure to the <see cref="PluginInfo"/> property.
        /// </summary>
        /// <param name="raiseEvents">When true the <see cref="INotifyPropertyChanged.PropertyChanged"/> event is raised for
        /// each property that has changed.
        /// The property names of the <see cref="VstPluginInfo"/> class are prefixed with 'PluginInfo.'.</param>
        void AcceptPluginInfoData(bool raiseEvents);
    }
}