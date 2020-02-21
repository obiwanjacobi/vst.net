namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// Indicates that the object can be activated and deactivated.
    /// </summary>
    public interface IActivatable
    {
        /// <summary>
        /// Gets or sets the current activation state.
        /// </summary>
        bool IsActive { get; set; }
    }
}
