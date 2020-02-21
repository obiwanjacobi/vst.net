namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface should be implemented when the plugin want to support Programs.
    /// </summary>
    /// <remarks>The implementor must initialize the plugin programs as appropriate. 
    /// Each <see cref="VstProgram"/> contains a full list of plugin Parameters.</remarks>
    public interface IVstPluginPrograms
    {
        /// <summary>
        /// Gets all the Programs.
        /// </summary>
        VstProgramCollection Programs { get; }
        /// <summary>
        /// Gets or sets the program that is currently active in the Plugin.
        /// </summary>
        /// <remarks>Note to implementor: You must not return null. 
        /// When no program is active, activate the first in the list.
        /// A null value can be set.</remarks>
        VstProgram ActiveProgram { get; set; }
        /// <summary>
        /// Called by the host just before a Program is set (activated).
        /// </summary>
        void BeginSetProgram();
        /// <summary>
        /// Called by the host just after a Program is set (activated).
        /// </summary>
        void EndSetProgram();
    }
}
