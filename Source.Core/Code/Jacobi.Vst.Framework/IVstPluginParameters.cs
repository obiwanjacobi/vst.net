namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface is implemented to publish information about the Parameters that control the plugin.
    /// </summary>
    /// <remarks>When you also support Programs you do not need to implement this interface explicitly. <seealso cref="VstProgram"/></remarks>
    public interface IVstPluginParameters
    {
        /// <summary>
        /// Gets the available parameter categories.
        /// </summary>
        VstParameterCategoryCollection Categories { get; }
        /// <summary>
        /// Gets the parameter values.
        /// </summary>
        VstParameterCollection Parameters { get; }
    }
}
