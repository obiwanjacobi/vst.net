namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// Indicates that the object can be activated and deactivated.
    /// </summary>
    public interface IActivatable
    {
        /// <summary>
        /// Gets the current activation state (true).
        /// </summary>
        bool IsActive { get; set; }
    }
}
