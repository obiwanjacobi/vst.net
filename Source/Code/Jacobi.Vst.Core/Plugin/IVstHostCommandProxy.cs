namespace Jacobi.Vst.Core.Plugin
{
    using System;

    /// <summary>
    /// The Vst Host Stub called by the Plugin (Framework).
    /// </summary>
    public interface IVstHostCommandProxy : IDisposable
    {
        /// <summary>
        /// Updates the new <paramref name="pluginInfo"/> with the host.
        /// </summary>
        /// <param name="pluginInfo">Must not be null.</param>
        /// <returns>Returns true if the update was successful.</returns>
        bool UpdatePluginInfo(VstPluginInfo pluginInfo);

        IVstHostCommands20 Commands { get; }
    }
}
