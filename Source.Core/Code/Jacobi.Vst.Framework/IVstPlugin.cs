namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// This interface is implemented by the Plugin root object.
    /// </summary>
    public interface IVstPlugin : IExtensible, IDisposable
    {
        /// <summary>
        /// Retrieves product information for the plugin (instance).
        /// </summary>
        VstProductInfo ProductInfo { get; }
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        string Name { get;}
        /// <summary>
        /// Gets the category of the plugin.
        /// </summary>
        VstPluginCategory Category { get; }
        /// <summary>
        /// Gets the additional capabilities of the plugin.
        /// </summary>
        /// <remarks>Almost all plugin capabilities in VST.NET are expressed by implementing interfaces. 
        /// But some characteristics of a plugin do not map to an interface. <seealso cref="VstPluginCapabilities"/></remarks>
        VstPluginCapabilities Capabilities { get; }
        /// <summary>
        /// Gets the initial delay (in samples??)
        /// </summary>
        int InitialDelay { get; }
        /// <summary>
        /// Gets the unique identifier of the plugin represented as a 4 character code.
        /// </summary>
        /// <remarks>This method will be refactored in a fututre version to return a string.</remarks>
        int PluginID { get; }
        /// <summary>
        /// The host will call this method when the plugin is loaded and should open its resources.
        /// </summary>
        /// <param name="host">A reference to the Host root interface. This reference can be used to 
        /// query for other host interfaces. Must not be null.</param>
        /// <remarks>Open is a good time to allocate large memory blocks when you use a pre-allocated 
        /// memory scheme in your plugin. The <see cref="IDisposable.Dispose"/> method will be called just before 
        /// your plugin is unloaded to release allocated resources.</remarks>
        void Open(IVstHost host);
        /// <summary>
        /// Called by the host when the user has turned off your plugin.
        /// </summary>
        /// <remarks>Your plugin is NOT being unloaded by the host. The user just turned it off.</remarks>
        void Suspend();
        /// <summary>
        /// Called by the host when the user has turned on your plugin.
        /// </summary>
        /// <remarks>Your plugin can receive multiple <see cref="Suspend"/>/Resume calls during its lifetime.</remarks>
        void Resume();
    }
}
