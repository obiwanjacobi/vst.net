namespace Jacobi.Vst.Core.Plugin
{
    using System;

    /// <summary>
    /// The Vst Host Stub called by the Plugin (Framework).
    /// </summary>
    public interface IVstHostCommandStub : IVstHostCommands20, IDisposable
    {
        /// <summary>
        /// Indicates if the host stub is fully initialized and is ready to receive commands.
        /// </summary>
        /// <returns>Returns true when initialized.</returns>
        bool IsInitialized();

        /// <summary>
        /// Updates the new <paramref name="pluginInfo"/> with the host.
        /// </summary>
        /// <param name="pluginInfo">Must not be null.</param>
        /// <returns>Returns true if the update was successful.</returns>
        bool UpdatePluginInfo(VstPluginInfo pluginInfo);
    }
}
