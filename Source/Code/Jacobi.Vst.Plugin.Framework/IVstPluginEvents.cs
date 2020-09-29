using System;

namespace Jacobi.Vst.Plugin.Framework
{
    /// <summary>
    /// Events for the plugin root object.
    /// </summary>
    public interface IVstPluginEvents
    {
        /// <summary>
        /// Is triggered when the plugin is openend.
        /// </summary>
        event EventHandler? Opened;
    }
}
