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
        bool IsActive { get; }
        /// <summary>
        /// Activates the object (does nothing when already active).
        /// </summary>
        void Activate();
        /// <summary>
        /// Deactivates the object (does nothing when already inactive).
        /// </summary>
        void Deactivate();
    }
}
